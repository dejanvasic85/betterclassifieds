using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.DataModel;
using BetterClassified.UIController.ViewObjects;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;
using Paramount.Common.UI.WebContent;

namespace BetterClassified.UIController.Repository
{
    public partial class LinqDaoRepository : IDataRepository
    {
        public object GetAppSetting(string settingName)
        {
            using (var db = BetterclassifiedsDataContext.NewContext())
            {
                var setting = db.AppSettings.Where(aps => aps.AppKey == settingName).FirstOrDefault();
                if (setting != null)
                    return setting.SettingValue;
                return null;
            }
        }
    }
}
