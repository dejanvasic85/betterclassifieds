using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController.ViewObjects
{
    public class LineAdColourVo
    {
        public int? LineAdColourId { get; set; }
        public string LineAdColourName { get; set; }
        public string ColourCode { get; set; }
        public int Cyan { get; set; }
        public int Magenta { get; set; }
        public int Yellow { get; set; }
        public int KeyCode { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
    }
}