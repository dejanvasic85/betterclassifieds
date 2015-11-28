using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController
{
    /// <summary>
    /// Betterclassified client specific settings can be read by accessing static properties directly.
    /// </summary>
    /// <remarks>
    /// This class is designed to be used for reading the betterclassified database App Setting table,
    /// however it can also be extended to use other configuration storage mechanisms.
    /// 
    /// The actual data fetching is still performed at the controller level which decides to use direct
    /// LINQ connection or a service connection.
    /// </remarks>
    public static class ClientSetting
    {
        private static object GetValue(string settingName, bool suppresException = false)
        {
            var value = new DataController().GetAppSetting(settingName);
            if (value == null && !suppresException)
                throw new Exception(string.Format("Setting name [{0}] from Client Settings.", settingName));
            return value;
        }

        /// <summary>
        /// Maximum number of characters allowed in a line ad word
        /// </summary>
        public static int LineAdWordMaxCharLength
        {
            get
            {
                return Convert.ToInt32(GetValue("WordMaxLength"));
            }
        }

        /// <summary>
        /// Characters that are used to separate words in line ads
        /// </summary>
        public static char[] LineAdWordSeperators
        {
            get
            {
                return GetValue("WordSeparators").ToString().ToCharArray();
            }
        }

        public static int LineAdHeadingCharMaxLength
        {
            get { return Convert.ToInt32(GetValue("BoldHeadingLimit")); }
        }
    }
}
