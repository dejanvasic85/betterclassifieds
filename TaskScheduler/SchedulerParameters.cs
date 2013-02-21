using System;
using System.Collections.Generic;

namespace Paramount.Products.TaskScheduler
{
    public class SchedulerParameters:Dictionary<string , string >
    {
        public SchedulerParameters(IEnumerable<string> args)
        {
            foreach(var item in args)
            {
                var keyValue = item.Split(new[]{"/"}, StringSplitOptions.None);
                if(keyValue.Length == 2)
                Add(keyValue[0].ToUpper(), keyValue[1]);
            }
        }
    }
}
