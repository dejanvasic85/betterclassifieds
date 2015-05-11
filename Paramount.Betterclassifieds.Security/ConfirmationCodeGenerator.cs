using Paramount.Betterclassifieds.Business;
using System;
using System.Text;

namespace Paramount.Betterclassifieds.Security
{
    public class ConfirmationCodeGenerator : IConfirmationCodeGenerator
    {
        private const int CodeLength = 4;

        public string GenerateCode()
        {
            var code = new StringBuilder();
            code.Append( new Random().Next(0, 9999).ToString());
            
            var zeroGapFill = CodeLength - code.Length;

            while (zeroGapFill > 0)
            {
                code.Append("0");
                zeroGapFill--;
            }

            return code.ToString();
        }
    }
}
