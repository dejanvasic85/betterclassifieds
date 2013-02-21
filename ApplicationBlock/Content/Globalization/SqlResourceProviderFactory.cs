namespace Paramount.ApplicationBlock.Content.Globalization
{
    using System;
    using System.Web.Compilation;

    public class SqlResourceProviderFactory : ResourceProviderFactory
    {
        public SqlResourceProviderFactory()
        {
        }

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return new SqlResourceProvider(null, classKey);
        }
        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            virtualPath = System.IO.Path.GetFileName(virtualPath);
            return new SqlResourceProvider(virtualPath, null);
        }

    }
}