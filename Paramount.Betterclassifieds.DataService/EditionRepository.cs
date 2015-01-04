namespace Paramount.Betterclassifieds.DataService.Repository
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Print;

    public class EditionRepository : IEditionRepository, IMappingBehaviour
    {
        public void DeleteEditionByDate(DateTime editionDate)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
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