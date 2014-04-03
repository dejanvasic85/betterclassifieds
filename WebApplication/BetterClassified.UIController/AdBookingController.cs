using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BetterclassifiedsCore.BusinessEntities;
using Paramount.ApplicationBlock.Data;
using Paramount.Utility.DataAccess;

namespace BetterClassified.UIController
{
    public class AdBookingController
    {
        const string configSection = "paramount/services";
        const string betterclassifiedConnection = "BetterclassifiedsConnection";

        public List<BookingSearchResult> SearchAdBookings(string sortBy, ObjectDataSourceSelectingEventArgs e, 
            int? adBookingId, string bookReference, string userName, DateTime? bookingStartDate, DateTime? bookingEndDate,
            int? bookingStatus, int? publicationId, int? parentCategoryid, int? subCategoryid,
            DateTime? editionStartDate, DateTime? editionEndDate, string adSearchText,
            int maximumRows, int startRowIndex)
        {
            List<BookingSearchResult> searchResults = new List<BookingSearchResult>();

            // Default the sort by to be AdBookingId
            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "AdBookingId DESC";
            }

            SqlCommand command = new SqlCommand()
            {
                CommandText = "psp_AdBookings_Select",
                CommandTimeout = 20,
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@AdBookingId", SqlDbType.Int).Value = adBookingId;
            command.Parameters.Add("@BookReference", SqlDbType.VarChar).Value = bookReference;
            command.Parameters.Add("@Username", SqlDbType.VarChar).Value = userName;
            command.Parameters.Add("@BookingDateStart", SqlDbType.DateTime).Value = bookingStartDate;
            command.Parameters.Add("@BookingDateEnd", SqlDbType.DateTime).Value = bookingEndDate;
            command.Parameters.Add("@BookingStatus", SqlDbType.Int).Value = bookingStatus;
            command.Parameters.Add("@PublicationId", SqlDbType.Int).Value = publicationId;
            command.Parameters.Add("@ParentCategoryId", SqlDbType.Int).Value = parentCategoryid;
            command.Parameters.Add("@SubCategoryid", SqlDbType.Int).Value = subCategoryid;
            command.Parameters.Add("@EditionStartDate", SqlDbType.Int).Value = editionStartDate;
            command.Parameters.Add("@EditionEndDate", SqlDbType.Int).Value = editionEndDate;
            command.Parameters.Add("@AdSearchText", SqlDbType.VarChar).Value = adSearchText;
            command.Parameters.Add("@StartRowIndex", SqlDbType.Int).Value = startRowIndex;
            command.Parameters.Add("@MaximumRows", SqlDbType.Int).Value = maximumRows;
            command.Parameters.Add("@SortExpression", SqlDbType.VarChar).Value = sortBy;
           
            // Total Rows output parameter
            SqlParameter outputParameter = new SqlParameter { ParameterName = "@TotalRowCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            command.Parameters.Add(outputParameter);

            // Fetch the Data from the database
            DataTable dt = command.GetDataTable(ConfigReader.GetConnectionString(configSection, betterclassifiedConnection));

            string[] ignoreMappingProperties = new string[] { "RowNumber" };

            // Map the DataRecords into usable objects
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BookingSearchResult searchResult = new BookingSearchResult();
                    row.ConvertToObject(searchResult, true, ignoreMappingProperties);
                    searchResults.Add(searchResult);
                }
            }
            
            e.Arguments.TotalRowCount = Convert.ToInt32(command.Parameters["@TotalRowCount"].Value.ToString());

            return searchResults;
        }

        public static int SearchAdBookingsCount(ObjectDataSourceSelectingEventArgs e)
        {
            return e.Arguments.TotalRowCount;
        }
    }
}
