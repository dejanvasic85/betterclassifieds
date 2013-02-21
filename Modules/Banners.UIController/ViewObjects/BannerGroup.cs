using System;

namespace Paramount.Banners.UIController.ViewObjects
{
    public class BannerGroup
    {
        public Guid  GroupId
        {
            get;
            set;
        }

        public string Title
        {
            get; 
            set;
        }

        public int? BannerHeight
        {
            get;
            set;
        }


        public int? BannerWidth
        {
            get;
            set;
        }

        public bool IncludeTimer
        {
            get;
            set;
        }

        public string ClientCode
        {
            get;
            set;
        }
    }
}
