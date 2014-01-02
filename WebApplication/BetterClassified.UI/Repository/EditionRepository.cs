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
                var editionsForDeletion = context.Editions.Where(e => e.EditionDate == editionDate).ToList();
                context.Editions.DeleteAllOnSubmit(editionsForDeletion);
                context.SubmitChanges();
            }
        }
      
        public void OnRegisterMaps(IConfiguration configuration)
        {
            
        }
    }
}