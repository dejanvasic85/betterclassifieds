using System.Collections;
using System.IO;
using System.Linq;

namespace Paramount.Betterclassifieds.Mvc.Modules
{
    /// <summary>
    /// 
    /// </summary>
    class VirtualFile : System.Web.Hosting.VirtualFile
    {
        public VirtualFile(string virtualPath, string mappedPath)
            : base(virtualPath)
        {
            _mappedPath = mappedPath;
        }
     

        public bool Exists
        {
            get { return File.Exists(_mappedPath); }
        }

        public override Stream Open()
        {
            return File.OpenRead(_mappedPath);
        }

        private string _mappedPath;
    }

    public class VirtualDirectory : System.Web.Hosting.VirtualDirectory
    {
        private readonly string virtualPath;
        private readonly string fullPath;

        private readonly ArrayList children = new ArrayList();
        private readonly ArrayList directories = new ArrayList();
        private readonly ArrayList files = new ArrayList();

        protected void LoadData()
        {
            directories.AddRange(Directory.GetDirectories(fullPath).Select(d => new VirtualDirectory(virtualPath, d)).ToList());
            files.AddRange(Directory.GetFiles(fullPath).Select(f => new VirtualFile(virtualPath, f)).ToList());
            children.AddRange(directories.ToArray().Union(files.ToArray()).ToList());
            
        }

        public VirtualDirectory(string virtualPath, string fullPath) : base(virtualPath)
        {
            this.virtualPath = virtualPath;
            this.fullPath = fullPath;
            LoadData();
        }

        public override IEnumerable Directories
        {
            get
            {
                return directories;
            }
        }

        public override IEnumerable Files
        {
            get { return files; }
        }

        public override IEnumerable Children
        {
            get { return children; }
        }
    }

   
}