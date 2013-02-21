using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterClassified.UIController.Repository;

namespace BetterClassified.UIController
{
    public static class DataFactory
    {
        public static IDataRepository CreateDataRepository(RepositoryType repositoryType)
        {
            switch (repositoryType)
            {
                case RepositoryType.LinqDao:
                    return new LinqDaoRepository();

                default:
                    throw new NotImplementedException(string.Format("Repository Type [{0}] does not have an implementation", repositoryType));
            }
        }
    }
}
