namespace Paramount.ApplicationBlock.Content.Globalization
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Resources;

    sealed class SqlResourceReader : IResourceReader
    {
        private  IDictionary _resources;
        public SqlResourceReader(IDictionary resources)
        {
            _resources = resources;
        }
        IDictionaryEnumerator IResourceReader.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }
        void IResourceReader.Close()
        {
            this._resources = null;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }
        void IDisposable.Dispose()
        {
        }
    }
}