namespace Paramount.Utility
{
    using System;

    public static class UrlPath
    {
        internal const char appRelativeCharacter = '~';
        internal const string appRelativeCharacterString = "~/";

        private static char[] s_slashChars = new char[] { '\\', '/' };

        internal static bool IsRooted(String basepath)
        {
            return (String.IsNullOrEmpty(basepath) || basepath[0] == '/' || basepath[0] == '\\');
        }

        // Returns whether the virtual path is relative.  Note that this returns true for 
        // app relative paths (e.g. "~/sub/foo.aspx")
        internal static bool IsRelativeUrl(string virtualPath)
        {
            // If it has a protocol, it's not relative
            if (virtualPath.IndexOf(":", StringComparison.Ordinal) != -1)
                return false;

            return !IsRooted(virtualPath);
        }


        internal static bool IsAppRelativePath(string path)
        {

            if (path == null)
                return false;

            int len = path.Length;

            // Empty string case 
            if (len == 0) return false;

            // It must start with ~
            if (path[0] != appRelativeCharacter)
                return false;

            // Single character case: "~" 
            if (len == 1)
                return true;

            // If it's longer, checks if it starts with "~/" or "~\"
            return path[1] == '\\' || path[1] == '/';
        }

        internal static bool IsValidVirtualPathWithoutProtocol(string path)
        {
            if (path == null)
                return false;
            if (path.IndexOf(":", StringComparison.Ordinal) != -1)
                return false;
            return true;
        }
    }
}