using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using Paramount.Common.DataTransferObjects;

namespace Paramount.Banners.UIController.ViewObjects
{
    [Serializable]
    public class BannerParameters
    {
        public BannerParameters()
        {
            OtherParams = new List<CodeDescription>();
        }

        public string Category { get; set; }
        public string Location { get; set; }
        public List<CodeDescription> OtherParams { get; set; }

        public Collection<CodeDescription> GetAllParams()
        {
            var param = new Collection<CodeDescription>
                            {
                                new CodeDescription() {Code = Constants.CategoryParam, Description = this.Category},
                                new CodeDescription() {Code = Constants.LocationParam, Description = this.Location}
                            };

            foreach (var codeDescription in OtherParams)
            {
                if (string.IsNullOrEmpty(codeDescription.Code) || string.IsNullOrEmpty(codeDescription.Description))
                {
                    continue;
                }
                param.Add(codeDescription);
            }
            return param;
        }

    }

}
