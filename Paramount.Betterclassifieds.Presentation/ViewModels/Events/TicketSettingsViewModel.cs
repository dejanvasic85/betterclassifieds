using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class TicketSettingsViewModel  : IMappingBehaviour
    {
        public TicketSettingsViewModel()
        {
            
        }

        public TicketSettingsViewModel(EventModel eventDetails)
        {
            this.Map(eventDetails, this);
        }

        public bool IncludeTransactionFee { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd H:mm")]
        public DateTime? ClosingDate { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-dd H:mm")]
        public DateTime? OpeningDate { get; set; }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventModel, TicketSettingsViewModel>();
        }
    }
}