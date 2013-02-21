using System;
using BetterclassifiedsCore;

namespace BetterClassified.UIController
{
    public static class EmailNotificationController
    {
        public static void SendNewBookingNotificationEmail(string referenceId)
        {
            var content = BookingController.GetBookingStringContentByRef(referenceId);
            // get the list of email addresses from the Application Setting 
            //var emailsCollection  = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_AdminEmails)

            
        }
    }
}
