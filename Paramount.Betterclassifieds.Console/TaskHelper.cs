namespace Paramount.Betterclassifieds.Console
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TaskHelper
    {
        public static bool DisplayHelp(ICollection<string> args)
        {
            if (args.Count > 0 && args.Any(a => a.StartsWith("help")))
            {
                System.Console.WriteLine("---- Available Tasks ----");
                TypeRegistrations.ActionEach(type => System.Console.WriteLine("{0}\t {1} ", type.Name, type.GetCustomAttribute<HelpAttribute>().Description));
                return true;
            }

            return false;
        }
    }
}