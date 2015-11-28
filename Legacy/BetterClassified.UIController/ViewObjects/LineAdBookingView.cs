using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController.ViewObjects
{
    [Serializable]
    public class LineAdBookingView
    {
        public int LineAdId { get; set; }
        public int NumberOfUnits { get; set; }
        public bool BoldHeading { get; set; }
        public bool SuperBoldHeading { get; set; }
        public bool ColourHeading { get; set; }
        public bool ColourBorder { get; set; }
        public bool ColourBackground { get; set; }
        public bool MainImage { get; set; }
        public bool SecondaryImage { get; set; }
    }
}
