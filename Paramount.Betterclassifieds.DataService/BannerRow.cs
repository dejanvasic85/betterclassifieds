using System;
using System.Data;

namespace Paramount.Betterclassifieds.DataService
{
    public class BannerRow
    {
        private readonly DataRow _row;

        public BannerRow (DataRow row )
        {
            _row = row;
            }

        public string Title
        {
            get
            {
                return _row.Field<string>("Title");
            }
        }

        public Guid BannerId { get
        {
            return _row.Field<Guid>("BannerId");
        }}

        public DateTime StartDate { get
        {
            return _row.Field<DateTime>("StartDateTime");
        }}

        public DateTime EndDate { get { return _row.Field<DateTime>("EndDateTime"); } }

        public Guid ImageId
        {
            get
            {
                return _row.Field<Guid>("DocumentID");
            }
        }

        public Guid GroupId { get { return _row.Field<Guid>("BannerGroupId"); } }

        public string NavigateUrl { get { return _row.Field<string>("NavigateUrl"); } }

        public int RequestCount { get { return _row.Field<int>("RequestCount"); } }

        public bool IsDeleted { get { return _row.Field<bool>("IsDeleted"); } }

        public string CreatedBy { get { return _row.Field<string>("CreatedBy"); } }

        public int ClickCount { get { return _row.Field<int>("ClickCount"); } }

        public string ClientCode { get { return _row.Field<string>("ClientCode"); } }
    }
}