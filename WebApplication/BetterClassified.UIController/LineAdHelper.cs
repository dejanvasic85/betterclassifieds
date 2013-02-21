using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController
{
    public class LineAdHelper
    {
        public static int GetWordCount(string adText)
        {
            var maxCharLength = ClientSetting.LineAdWordMaxCharLength;
            // Get all words split by client setting
            var wordList = adText.Split(ClientSetting.LineAdWordSeperators).Where(i => !i.Equals(string.Empty)).ToList();
            // Get all words that exceed the max character length
            var maxWordExceedList = wordList.Where(w => w.Length > maxCharLength).ToList();
            int extraWords = maxWordExceedList.Sum(w => w.Length / maxCharLength);
            int totalWordCount = wordList.Count - maxWordExceedList.Count + extraWords;
            return totalWordCount;
        }
    }
}
