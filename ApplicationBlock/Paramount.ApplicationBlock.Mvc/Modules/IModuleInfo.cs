using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.ApplicationBlock.Mvc
{
    public interface IModuleInfo
    {
        string PhysicalPath { get; }

        string Name { get; }

        string VirtualPath { get;}
        string Namespace { get; }
        bool RegisterView { get; }
    }
}
