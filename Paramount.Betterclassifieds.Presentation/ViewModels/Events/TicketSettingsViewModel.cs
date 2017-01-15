using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class TicketSettingsViewModel  : IMappingBehaviour, IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (OpeningDate.HasValue && ClosingDate.HasValue && OpeningDate.Value > ClosingDate.Value)
            {
                errors.Add(new ValidationResult("Closing date must be after the opening date", new [] {"ClosingDate"}));
            }
            return errors;
        }
    }
}