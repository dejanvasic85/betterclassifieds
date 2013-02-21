using System;

namespace Paramount.DSL.UI
{
    public class UploadCompleteEventArgs : EventArgs
    {
        public UploadCompleteEventArgs(string documentId)
        {
            DocumentId = documentId;
        }

        protected string DocumentId { get; set; }
    }
}