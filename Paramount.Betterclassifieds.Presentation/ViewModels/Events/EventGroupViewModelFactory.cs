using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGroupViewModelFactory : IMappingBehaviour
    {
        public EventGroupViewModel ToViewModel(EventGroup group)
        {
            return this.Map<EventGroup, EventGroupViewModel>(group);
        }

        public IEnumerable<EventGroupViewModel> ToViewModels(IEnumerable<EventGroup> groups)
        {
            return groups.Select(ToViewModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            GetMapping(configuration);
        }

        public static EventGroupViewModelFactory Map()
        {
            return new EventGroupViewModelFactory();
        }

        public static IMappingExpression<EventGroup, EventGroupViewModel> GetMapping(IConfiguration mappingConfig)
        {
            return mappingConfig.CreateMap<EventGroup, EventGroupViewModel>();
        }
    }
}