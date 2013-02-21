using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterClassified.UIController.Repository;
using System.Configuration;

namespace BetterClassified.UIController
{
    public abstract class BaseController
    {
        protected IDataRepository _dataContext;

        public BaseController()
        {
            // Default to Linq
            RepositoryType repositoryType = RepositoryType.LinqDao;

            // Get the configured default setting
            string strategy = ConfigurationManager.AppSettings.Get("DefaultDaoFactory");

            if (!string.IsNullOrEmpty(strategy))
            {
                switch(strategy.ToLower())
                {
                    case "linqdao":
                        repositoryType = RepositoryType.LinqDao;
                        break;
                }
            }

            initialiseDataContext(repositoryType);
        }

        public BaseController(RepositoryType repositoryType)
        {
            initialiseDataContext(repositoryType);
        }

        private void initialiseDataContext(RepositoryType repositoryType)
        {
            _dataContext = DataFactory.CreateDataRepository(repositoryType);
        }
    }
}
