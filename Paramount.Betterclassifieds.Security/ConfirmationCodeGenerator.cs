namespace Paramount.Betterclassifieds.Security
{
    using System;
    using System.Text;

    using Business;
    using Utility;

    public class ConfirmationCodeGenerator : IConfirmationCodeGenerator
    {
        private readonly IDateService _dateService;
        public const int CodeLength = 4;

        public ConfirmationCodeGenerator(IDateService dateService)
        {
            _dateService = dateService;
        }

        public ConfirmationCodeResult GenerateCode()
        {

            var code = new StringBuilder();
            code.Append(new Random().Next(0, 9999).ToString());

            var zeroGapFill = CodeLength - code.Length;

            while (zeroGapFill > 0)
            {
                code.Append("0");
                zeroGapFill--;
            }

            return new ConfirmationCodeResult(code.ToString(),
                expiry: _dateService.Now.AddMinutes(10),
                expiryUtc: _dateService.UtcNow.AddMinutes(10));
        }
    }
}
