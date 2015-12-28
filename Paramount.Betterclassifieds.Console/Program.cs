using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Console
{

    class Program
    {
        private IUnityContainer _container;
        private static readonly ILogger _logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            Program program = new Program();
            program.RegisterContainer();
            
            try
            {

#if DEBUG
                program.Start(new[]
                {
                    TaskArguments.TaskFullArgName, typeof(Tasks.EmailProcessor).Name, "-take", "20"
                });

#else
                program.Start(args);
#endif
            }
            catch (Exception ex)
            {

                // If it gets to here then some unexpected exception occurred within a job.
                // Log to the windows event viewer as last resort.
                _logger.Error(ex);
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

            var taskArguments = TaskArguments.FromArray(args);

            // Attempt to locate the appropriate task
            var task = _container.ResolveAll<ITask>().SingleOrDefault(t => t.GetType().Name.Equals(taskArguments.TaskName, StringComparison.OrdinalIgnoreCase));

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

            _logger.Info("Processing command arguments...");

            // Handle the arguments
            task.HandleArgs(taskArguments);

            _logger.Info("Starting " + taskArguments.TaskName);

            // Run the task!
            task.Run();

            // Log the completion
            _logger.Info("Completed successfully");
        }

        // When registering your task ensure that your register them here
        public void RegisterContainer()
        {
            _container = new UnityContainer();

            UnityConfig.Initialise(_container);

            // Register all Tasks (anything that implements ITask)
            TypeRegistrations.ActionEach(task => _container.RegisterType(typeof(ITask), task, task.Name));
        }
    }
}
