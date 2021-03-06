﻿using System;

namespace Paramount.Betterclassifieds.Business.Payment
{
    public enum ResponseType
    {
        Success = 0,
        CardDeclined = 1,
        PayPalServerError = 2
    }

    public class ResponseFriendlyMessage
    {
        public static string Get(int? responseType)
        {
            if (responseType == null)
                return string.Empty;

            var enumVal = (ResponseType)responseType;

            switch (enumVal)
            {
                case ResponseType.CardDeclined:
                    return "Your card has been declined. Please try again and ensure all details are correct.";

                case ResponseType.PayPalServerError:
                    return "PayPal was not able to process the payment successfully. Please try again.";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}