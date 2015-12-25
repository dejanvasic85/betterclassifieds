using System;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    /// <summary>
    /// Maps text parameters from the business scenario text to primitive types
    /// </summary>
    public static class ArgumentParser
    {
        public static bool Is(string isOrIsNot)
        {
            if (isOrIsNot.ToLower().Equals("is"))
            {
                return true;
            }

            if (isOrIsNot.ToLower().Equals("is not"))
            {
                return false;
            }

            throw new ArgumentException("Parameter does not contain is or is not text", isOrIsNot);
        }
    }
}
