using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UI.Dsl
{
    public class SelectedDocumentArgs : EventArgs
    {
        public string DocumentID;

        public SelectedDocumentArgs(string documentId)
        {
            this.DocumentID = documentId;
        }
    }
}
