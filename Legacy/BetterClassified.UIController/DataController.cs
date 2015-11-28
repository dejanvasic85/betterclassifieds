using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BetterClassified.UIController.Repository;
using BetterclassifiedsCore.DataModel;
using System.Reflection;
using BetterClassified.UIController.ViewObjects;

namespace BetterClassified.UIController
{
    public class DataController : BaseController
    {
        public DataController() { }

        public DataController(RepositoryType repositoryType) : base(repositoryType) { }

        public object GetAppSetting(string settingName)
        {
            return _dataContext.GetAppSetting(settingName);
        }
    }
}
