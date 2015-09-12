using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Console
{

    class Program
    {
        private IUnityContainer _container;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.RegisterContainer();

            try
            {

#if DEBUG
                program.Start(new[]
                {
                    TaskArguments.TaskFullArgName, typeof(Tasks.EmailProcessor).Name
                });

#else
                program.Start(args);
#endif
            }
            catch (Exception ex)
            {

                // If it gets to here then some unexpected exception occurred within a job.
                // Log to the windows event viewer as last resort.
                LogError(ex.Message);
                LogError(ex.StackTrace);
#if !DEBUG
                ex.ToEventLog();
#endif
            }
        }

        public void Start(string[] args)
        {
            // If the arguments contains "Help" then get the task helper to do the work
            if (TaskHelper.DisplayHelp(args))
                return;

            TaskArguments taskArguments = TaskArguments.FromArray(args);

            // Attempt to locate the appropriate task
            ITask task = _container.ResolveAll<ITask>().SingleOrDefault(t => t.GetType().Name.Equals(taskArguments.TaskName, StringComparison.OrdinalIgnoreCase));

            if (task == null)
                throw new ArgumentException(string.Format("Task name [{0}] does not exist.", taskArguments.TaskName), "args");

            // Check if only single instances can be invoked
            if (task.Singleton)
            {
                if (SingleInstance.IsRunning(taskArguments.TaskName))
                {
                    throw new ArgumentException(string.Format("[{0}] is a singleton task and is already running.", taskArguments.TaskName));
                }
            }

            LogInfo("Processing command arguments...");

            // Handle the arguments
            task.HandleArgs(taskArguments);

            LogInfo("Starting " + taskArguments.TaskName);

            // Run the task!
            task.Run();
            LogInfo("Completed successfully");
        }

        // When registering your task ensure that your register them here
        public void RegisterContainer()
        {
            _container = new UnityContainer();

            UnityConfig.Initialise(_container);

            // Register all Tasks (anything that implements ITask)
            TypeRegistrations.ActionEach(task => _container.RegisterType(typeof(ITask), task, task.Name));
        }

        private static void LogInfo(string message)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }

        private static void LogError(string message)
        {
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }
    }
}
