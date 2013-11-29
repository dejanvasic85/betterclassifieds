using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Optimization;

namespace Paramount.ApplicationBlock.Mvc
{
    /// <summary>
    /// Virtual path provider so view modules can store their views (.aspx, .ascx etc) in a different (app relative) disk location 
    /// </summary>
    /// <remarks>
    ///  UNIFY CHANGE: added feature
    /// </remarks>
    public class ViewModulesVirtualPathProvider : VirtualPathProvider
    {
        #region public 
     
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (virtualPathDependencies == null)
                return null;

            string mappedPath = GetMappedPath(virtualPath);

            if (mappedPath != null)
            {
                var fullPath = new List<string>();
                foreach (string virtualPathDependency in virtualPathDependencies)
                {
                    fullPath.Add(virtualPathDependency);
                }

                return new CacheDependency(new[] { mappedPath }, fullPath.ToArray(), utcStart);
            }


            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies,  utcStart);
        }
      
        public override bool DirectoryExists(string virtualDir)
        {
            string mappedPath = GetMappedPath(virtualDir);
            if (mappedPath != null)
            {
                var d = Directory.Exists(mappedPath);
                return d;
            }
            return base.DirectoryExists(virtualDir);
        }

        public class CustomScriptBundle : ScriptBundle
        {
            public CustomScriptBundle(string virtualPath) : base(virtualPath)
            {
            }


            public CustomScriptBundle(string virtualPath, string cdnPath) : base(virtualPath, cdnPath)
            {
            }
        }

        public class Ob : System.Web.Optimization.Bundle
        {
            public Ob(): base()
            {
            }
        }

      

        /// <summary>
        /// Does file exists at vpath?
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override bool FileExists(string virtualPath)
        {
            string mappedPath = GetMappedPath(virtualPath);

            if (mappedPath != null)
            {
                return (File.Exists(mappedPath) ? true : false);
            }
            else
            {
                return base.FileExists(virtualPath);
            }
        }


        public override System.Web.Hosting.VirtualDirectory GetDirectory(string virtualDir)
        {
            string mappedPath = GetMappedPath(virtualDir);
            if (mappedPath != null)
            {
                return new VirtualDirectory(virtualDir, mappedPath);
            }

            return base.GetDirectory(virtualDir);
        }

        /// <summary>
        /// Return the v-file
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override System.Web.Hosting.VirtualFile GetFile(string virtualPath)
        {
            string mappedPath = GetMappedPath(virtualPath);

            if (mappedPath != null)
            {
                return new VirtualFile(virtualPath, mappedPath);
            }
            else
            {
                return Previous.GetFile(virtualPath);
            }
        }

       
        #endregion

        #region internal static

        /// <summary>
        /// Register a view-module's view mapping path
        /// </summary>
        /// <param name="moduleInfo">view module to register for virtual path mapping</param>
        /// <remarks>called from ViewModuleInitializer</remarks>
        internal static void RegisterViewModule(IModuleInfo moduleInfo)
        {
            if (string.IsNullOrEmpty(moduleInfo.PhysicalPath))
            {
                return;
            }

            var modulePhysicalPath = moduleInfo.PhysicalPath;
            if (!Path.IsPathRooted(modulePhysicalPath))
            {
                modulePhysicalPath = Path.Combine(HttpRuntime.AppDomainAppPath, modulePhysicalPath);
            }
            if (!Directory.Exists(modulePhysicalPath))
            {
                throw new DirectoryNotFoundException("MVC module '" + moduleInfo.Name + "': configured physicalPath directory is not found");
            }

            var moduleVirtualPath = moduleInfo.VirtualPath.Replace("~", "").ToLowerInvariant();

            Trace.TraceInformation("ViewModulesVirtualPathProvider: Registering virtualpath mapping for module '{0}': VirtualPath: '{1}', PhysicalPath: '{2}'", moduleInfo.Name, moduleVirtualPath, modulePhysicalPath);
            ModulePathMappings.Add(moduleVirtualPath, modulePhysicalPath);
        }

        /// <summary>
        /// number of view-module virtual paths registered
        /// </summary>
        internal static int ViewModulesCount
        {
            get { return ModulePathMappings.Count; }
        }

        internal static Dictionary<string, string> ViewModuleMappings
        {
            get { return ModulePathMappings; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        internal static string GetMappedAppPath(string virtualPath)
        {
            string mappedPath = GetMappedPath(virtualPath);
            if (mappedPath == null)
            {
                mappedPath = HttpRuntime.AppDomainAppPath + GetAppRelativeVirtualPath(virtualPath);
            }
            return mappedPath;
        }

        #endregion

        #region "private static"

        /// <summary>
        /// Get the physical file mapped path for the given virtual path
        /// </summary>
        /// <param name="virtualPath">virtual path</param>
        /// <returns>the physical path if it is a view-module mapped path, else null</returns>
        /// <remarks>
        /// 1. [Garth] This function is called *many* times and is very performance sensitive: 
        /// hence the memory cache. Ensure all changes are performance tested pre and post change!
        /// 
        /// 2. [Raj] - MSDN says you cannot store ASP.NET application folders or files that generate 
        /// application-level assemblies in a virtual file system. eg: 
        /// Global.asax/Sitemap/bin/AppCode/App_LocalResources/App_GlobaResources
        /// but this VPP stores some of these... no idea!
        /// </remarks> 
        private static string GetMappedPath(string virtualPath)
        {
            string mappedPath;
        
                if (System.Web.Optimization.BundleTable.Bundles.GetRegisteredBundles()
                    .Any(a => a.Path.Equals(virtualPath, StringComparison.InvariantCultureIgnoreCase)))
                {

                    var t =
                        System.Web.Optimization.BundleTable.Bundles.FirstOrDefault(
                            a => a.Path.Equals(virtualPath, StringComparison.InvariantCultureIgnoreCase));
                }

            //
            if (!VirtualPathMappings.TryGetValue(virtualPath, out mappedPath))
            {
                string appVirtualPath = GetAppRelativeVirtualPath(virtualPath).ToLowerInvariant();


                foreach (KeyValuePair<string, string> mapping in ModulePathMappings.Where(mapping => appVirtualPath.StartsWith(mapping.Key)))
                {
                    mappedPath = Path.GetFullPath((mapping.Value + appVirtualPath.ToLower().Replace(mapping.Key.ToLower(), "")).Replace("/", @"\"));
                    Trace.TraceInformation("ViewModulesVirtualPathProvider: mapped path '{0}' to: '{1}'", virtualPath, mappedPath);

                    lock (VirtualPathMappings)
                    {
                        if (!VirtualPathMappings.ContainsKey(virtualPath))
                        {
                            VirtualPathMappings.Add(virtualPath, mappedPath);
                        }
                    }
                    break;
                }
            }

            return mappedPath;
        }

        /// <summary>
        /// Adjust the virtual path to exclude the application-path, if not app relative
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        private static string GetAppRelativeVirtualPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
            {
                Trace.TraceWarning("GetAppRelativeVirtualPath contains null or empty virtual path");
            }

            if (!string.IsNullOrEmpty(virtualPath))
            {
                if (virtualPath.StartsWith("~"))
                {
                    virtualPath = virtualPath.Remove(0, 1);
                }
                else if (HttpRuntime.AppDomainAppVirtualPath != null)
                {

                    string appPath = HttpRuntime.AppDomainAppVirtualPath;
                    if (virtualPath != "/" && virtualPath.StartsWith(appPath))
                    {
                        virtualPath = virtualPath.Remove(0, appPath.Length);
                        if (!virtualPath.StartsWith("/"))
                        {
                            virtualPath = "/" + virtualPath;
                        }
                    }
                }
            }
           
            return virtualPath;
        }

        private static readonly Dictionary<string, string> ModulePathMappings = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        private static readonly Dictionary<string, string> VirtualPathMappings = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        #endregion

    }
}