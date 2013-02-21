namespace Paramount.DataService
{
    using System;
    using System.Data;

    struct Fields
    {
        public const string Username = "UserId";
        public const string BookingReference = "BookReference";
        public const string AdDesignId = "AdDesignId";
        public const string LastEditionDate = "LastEditionDate";
        public const string MainCategoryId = "MainCategoryId";
        public const string AdBookingId = "AdBookingId";
        public const string BookingDate = "BookingDate";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string TotalPrice = "TotalPrice";
    }
    public class ExpiredAdRow
    {
        private readonly DataRow _row;
        public ExpiredAdRow( DataRow row)
        {
            _row = row;
        }

        public string Username
        {
            get
            {
                return _row.Field<string>(Fields.Username);
            }
        }

        public string BookingReference
        {
            get
            {
                return _row.Field<string>(Fields.BookingReference);
            }
        }

        public int AdDesignId
        {
            get
            {
                return _row.Field<int>(Fields.AdDesignId);
            }
        }

        public DateTime LastEditionDate
        {
            get
            {
                return _row.Field<DateTime>(Fields.LastEditionDate);
            }
        }

        public int MainCategory
        {
            get
            {
                return _row.Field<int>(Fields.MainCategoryId);
            }
        }

        public int AdBookingId
        {
            get
            {
                return _row.Field<int>(Fields.AdBookingId);
            }
        }

        public DateTime BookingDate
        {
            get
            {
                return _row.Field<DateTime>(Fields.BookingDate);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _row.Field<DateTime>(Fields.StartDate);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _row.Field<DateTime>(Fields.EndDate);
            }
        }

        public Decimal TotalPrice
        {
            get
            {
                return _row.Field<decimal>(Fields.TotalPrice);
            }
        }
    }
}