namespace Paramount.Betterclassifieds.Console
{
    using Microsoft.Practices.Unity;
    using System;
    using System.Linq;
    using System.Reflection;
    using Tasks;

    class Program
    {
        private IUnityContainer _container;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.RegisterTasks();

            try
            {
#if DEBUG
                program.Start(new[]
                                    {
                                        TaskArguments.TaskFullArgName, "ExportDatabaseConfig",
                                    });
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
#else
                program.Start(args);
#endif
            }
            catch (Exception ex)
            {
                // If it gets to here then some unexpected exception occurred within a job.
                // Log to the windows event viewer as last resort.
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                ex.ToEventLog();
            }
        }

        // When registering your task ensure that your register them here
        public void RegisterTasks()
        {
            // Register one useful container with external service mappings
            _container = new UnityContainer();

            // Auto register the tasks for the current assembly (where the task should be declared)!
            Type taskInterface = typeof(ITask);
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(i => i.GetInterface(taskInterface.Name, false) == taskInterface)
                .ToList()
                .ForEach(task => _container.RegisterType(taskInterface, task, task.Name));
        }

        public void Start(string[] args)
        {
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

            // Handle the arguments
            task.HandleArgs(taskArguments);

            // Run the task!
            task.Run();
        }
    }
}
