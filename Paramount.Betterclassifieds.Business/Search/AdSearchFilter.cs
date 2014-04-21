using System.Text.RegularExpressions;

namespace Paramount.Betterclassifieds.Business.Search
{
    public class AdSearchFilter
    {
        private int? _adId;
        private bool _adIdEvaluated;

        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }
        public string SortOption { get; set; }

        public int? AdId
        {
            get
            {
                if (_adIdEvaluated)
                    return _adId;

                _adIdEvaluated = true;

                int id;

                // Ensure that only a number was provided
                if (int.TryParse(Keyword, out id))
                {
                    _adId = id;
                }

                // Ensure that publication id prefix format is supplied
                var regex = new Regex("^([0-9]*-[0-9]*){1}$");
                if (regex.IsMatch(Keyword))
                {
                    _adId = int.Parse(Keyword.Split('-')[1]);
                }

                return _adId;
            }
        }
    }
}