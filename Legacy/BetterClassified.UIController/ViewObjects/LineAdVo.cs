using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController.ViewObjects
{
    [Serializable]
    public class LineAdVo
    {
        public int LineAdId { get; set; }
        public int AdDesignid { get; set; }

        public string AdHeader { get; set; }
        public string AdText { get; set; }
        public string HeaderColourCode { get; set; }
        public string BorderColourCode { get; set; }
        public string BackgroundColourCode { get; set; }
        public string FooterPhotoId { get; set; }
        //public bool IsAdHeader { get; set; }
        //public int NumOfWords { get; set; }
        //public bool IsMainPhoto { get; set; }
        //public bool IsFooterPhoto { get; set; }
        //public bool IsColourBoldHeading { get; set; }
        //public bool IsColourBorder { get; set; }
        //public bool IsColourBackground { get; set; }
        public bool IsSuperBoldHeading { get; set; }

        public LineAdBookingView LineAdBookingView { get; set; }

        public LineAdVo()
        {
            LineAdBookingView = new LineAdBookingView { LineAdId = this.LineAdId };
        }
    }
}
