using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using BetterClassified.UI.Models;
using BetterClassified.UIController;
using BetterClassified.UIController.Booking;
using BetterclassifiedsCore;
using Microsoft.Practices.Unity;

namespace BetterClassified.UI
{
    public partial class AjaxWebService : WebService
    {
        [WebMethod(true)]
        public string GetBorderColourSuggestion(string headerColour, string backgroundColour)
        {
            return new LineAdController().GetLineAdBorderColourSuggestion(headerColour, backgroundColour);
        }

        [WebMethod(true)]
        public string GetHeaderColourSuggestion(string borderColour, string backgroundColour)
        {
            return new LineAdController().GetLineAdHeaderColourSuggestion(borderColour, backgroundColour);
        }

        [WebMethod(true)]
        public string GetBackgroundColourSuggestion(string headerColour, string borderColour)
        {
            return new LineAdController().GetLineAdBackgroundColourSuggestion(headerColour, borderColour);
        }

        [WebMethod(EnableSession = true)]
        public int GetAdWordCount(string adText)
        {
            // Store the number of words in to the session
            IBookCartContext bookCartContext = BookCartController.GetCurrentBookCart(true);

            int wordCount = LineAdHelper.GetWordCount(adText);

            bookCartContext.LineAdText = adText;
            bookCartContext.LineAdTextWordCount = wordCount;

            return wordCount;
        }

        [WebMethod(EnableSession = true)]
        public void LineAdBoldHeaderClicked(bool isHeaderSelected)
        {
            // Selects the line ad bold header option in session
            IBookCartContext bookCartContext = BookCartController.GetCurrentBookCart(true);

            if (bookCartContext != null)
            {
                bookCartContext.LineAdIsNormalAdHeading = isHeaderSelected;
            }
        }

        [WebMethod(EnableSession = true)]
        public void SuperBoldHeaderClicked(bool isSuperHeaderSelected)
        {
            // Selects the line ad bold header option in session
            IBookCartContext bookCartContext = BookCartController.GetCurrentBookCart(true);

            if (bookCartContext != null)
            {
                bookCartContext.LineAdIsSuperBoldHeading = isSuperHeaderSelected;
            }
        }

        [WebMethod(EnableSession = true)]
        public void LineAdBorderColourClicked(bool isSelected)
        {
            var bookingContext = BookCartController.GetCurrentBookCart();

            if (bookingContext != null)
                bookingContext.LineAdIsColourBorder = isSelected;
        }

        [WebMethod(EnableSession = true)]
        public void LineAdHeaderColourClicked(bool isSelected)
        {
            // Store the selection variable in to the session
            var bookingContext = BookCartController.GetCurrentBookCart(true);

            if (bookingContext != null)
                bookingContext.LineAdIsColourHeading = isSelected;
        }

        [WebMethod(EnableSession = true)]
        public void LineAdBackgroundColourClicked(bool isSelected)
        {
            // Store the selection variable in to the session
            var bookingContext = BookCartController.GetCurrentBookCart();

            if (bookingContext != null)
                bookingContext.LineAdIsColourBackground = isSelected;
        }

        [WebMethod(EnableSession = true)]
        public string GetPriceSummary()
        {
            StringWriter stringWriter = new StringWriter();

            AdPriceSummary adPriceSummary = new AdPriceSummary();

            var page = new Page();
            page.Controls.Add(adPriceSummary);

            Server.Execute(page, stringWriter, false);

            return stringWriter.ToString();
        }

        [WebMethod(EnableSession = true)]
        public void AddOrUpdateSubjectTag(string subject)
        {
            Unity.DefaultContainer.Resolve<Repository.ILookupRepository>().AddOrUpdate(LookupGroup.TutorSubjects, subject);
        }

        [WebMethod]
        public string[] GetSubjectLookups(string searchString)
        {
            return Unity.DefaultContainer.Resolve<Repository.ILookupRepository>()
                .GetLookupsForGroup(LookupGroup.TutorSubjects, searchString: searchString)
                .ToArray();
        }
    }
}