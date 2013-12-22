using System;
using System.Linq;
using System.Reflection;
using Paramount.Betterclassifieds.Console.Tasks;

namespace Paramount.Betterclassifieds.Console
{
    public static class TypeRegistrations
    {
        private static Type[] _tasks;

        public static Type[] AvailableTasks()
        {
            if (_tasks == null)
            {
                var taskInterface = typeof(ITask);
                _tasks = Assembly.GetExecutingAssembly()
                                 .GetTypes()
                                 .Where(i => i.GetInterface(taskInterface.Name, false) == taskInterface)
                                 .ToArray();
            }

            return _tasks;
        }

        public static void ActionEach(Action<Type> action)
        {
            AvailableTasks().ForEach(action);
        }
    }
}