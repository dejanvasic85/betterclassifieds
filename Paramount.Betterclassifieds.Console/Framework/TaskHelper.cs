using System;

namespace Paramount.Betterclassifieds.Console
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TaskHelper
    {
        public static bool DisplayHelp(ICollection<string> args)
        {
            if (args.Count == 0 || args.Any(a => a.StartsWith("help", StringComparison.OrdinalIgnoreCase)))
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("usage bc <command>\n");
                System.Console.WriteLine("where <command> is one of\n\n");

                TypeRegistrations.ActionEach(type =>
                {
                    var helpAttribute = type.GetCustomAttribute<HelpAttribute>();

                    if (helpAttribute != null)
                        System.Console.WriteLine("bc -TaskName {0} {1} ", type.Name, helpAttribute.SampleCall);

                });
                return true;
            }

            return false;
        }
    }
}