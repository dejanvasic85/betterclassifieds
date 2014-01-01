using System;
using System.Linq;
using AutoMapper;
using BetterclassifiedsCore.DataModel;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class EditionRepository : IEditionRepository, IMappingBehaviour
    {
        public void DeleteEditionByDate(DateTime editionDate)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var editionToDelete = context.Editions.First(e => e.EditionDate == editionDate);
                context.Editions.DeleteOnSubmit(editionToDelete);
                context.SubmitChanges();
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            
        }
    }
}