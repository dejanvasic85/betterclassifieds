using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business
{
    public class SetupException : Exception
    {
        public SetupException(string msg)
            : base(msg)
        { }
    }
}
