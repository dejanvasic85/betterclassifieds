using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Console
{
    public class TaskArguments : List<Tuple<string, string>>
    {
        public const string ArgumentPrefix = "-";
        public const string TaskArgName = "TaskName";
        public const string TaskFullArgName = ArgumentPrefix + TaskArgName;

        public string TaskName { get; private set; }

        /// <summary>
        /// Static constructor that creates instance of <see cref="TaskArguments"/> by parsing the array values
        /// </summary>
        public static TaskArguments FromArray(string[] values)
        {
            // Ensure that there are arguments and there's even number
            if (values == null || values.Length == 0 || values.Length % 2 == 1)
                throw new ArgumentException("Number of arguments is invalid.");

            // Ensure that TaskName is supplied
            if (!values.Any(v => v.Equals(TaskFullArgName, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException(string.Format("Task name argument must be supplied in format of {0} <Name>. Unable to start program.", TaskFullArgName));


            TaskArguments args = new TaskArguments();

            // Construct the container (list of tuples :)) with a regular loop
            for (int i = 0; i < values.Length; i++)
            {
                // Validate the argument set e.g    -Folder C:\bdk kdjfkj
                if (!values[i].StartsWith(ArgumentPrefix) || values.Length <= i + 1)
                {
                    throw new ArgumentException(string.Format("Bad argument {0}. Please specify arguments with a preceding [{1}] and value after it.", values[i], ArgumentPrefix));
                }

                if (values[i].Equals(TaskFullArgName, StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(values[i + 1]))
                        throw new ArgumentException("Task name has no value supplied.");

                    // Store the task name and continue
                    args.TaskName = values[i + 1];

                    i++;
                    continue;
                }

                // Add to the tuple arguments and strip the starting dash - no need for it
                args.Add(new Tuple<string, string>(values[i].TrimStart(ArgumentPrefix.ToCharArray()), values[i + 1]));
                i++;
            }

            return args;
        }

        /// <summary>
        /// Attempts to retrieve a specified value for specific type
        /// </summary>
        public T ReadArgument<T>(string name, bool isRequired = false, Func<T> readDefault = null)
        {
            var value = ReadArgument(name, isRequired);

            if (value == null && readDefault == null)
                throw new ArgumentException(string.Format("The argument {0} is required and there is no default defined.", name));

            if (value == null)
                return readDefault();

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Attempts to retrieve a specified value as a string
        /// </summary>
        public string ReadArgument(string name, bool isRequired = false)
        {
            var value = this.FirstOrDefault(a => a.Item1.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (value == null && isRequired)
                throw new ArgumentException(string.Format("The argument {0} is required", name));

            if (value == null)
                return null;

            var argumentValue = value.Item2;

            // Check that the value needs any substitution from other arguments
            if (argumentValue.Contains("{") && argumentValue.Contains("}"))
            {
                // For every argument - find anything that needs replacing
                foreach (Tuple<string, string> tuple in this)
                {
                    argumentValue = argumentValue.Replace("{" + tuple.Item1 + "}", tuple.Item2);
                }
            }

            return argumentValue;
        }
    }
}
