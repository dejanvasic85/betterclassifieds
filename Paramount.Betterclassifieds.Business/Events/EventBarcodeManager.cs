using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBarcodeValidator : IEventBarcodeValidator
    {
        private readonly IEventRepository _eventRepository;
        private const char CharSplitter = '-';

        public EventBarcodeValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public string GetDataForBarcode(int eventId, EventBookingTicket eventBookingTicket)
        {
            Guard.NotNull(eventBookingTicket);

            return $"{eventId}{CharSplitter}{eventBookingTicket.EventTicketId}{CharSplitter}{eventBookingTicket.EventBookingTicketId}";
        }

        public EventBookingTicketValidationResult ValidateTicket(string barcodeData)
        {
            Guard.NotNull(barcodeData);

            var dataFields = barcodeData.Split(CharSplitter);
            if (dataFields.Length != 3)
                return EventBookingTicketValidationResult.BadData();

            var dataFieldsAsNumbers = dataFields.ToInt();
            if (dataFieldsAsNumbers.Any(n => n == null))
                return EventBookingTicketValidationResult.BadData();

            var eventId = dataFieldsAsNumbers[0].GetValueOrDefault();
            var eventTicketId = dataFieldsAsNumbers[1].GetValueOrDefault();
            var eventBookingTicketId = dataFieldsAsNumbers[2].GetValueOrDefault();

            var eventModel = _eventRepository.GetEventDetails(eventId);

            if (eventModel == null)
                return EventBookingTicketValidationResult.NoSuchEvent(eventId);

            var eventTicket = eventModel.Tickets.SingleOrDefault(t => t.EventTicketId == eventTicketId);
            if (eventTicket == null)
                return EventBookingTicketValidationResult.NoSuchTicket(eventId, eventTicketId);

            var eventBookingTicket = _eventRepository.GetEventBookingTicket(eventBookingTicketId);
            if (eventBookingTicket == null)
                return EventBookingTicketValidationResult.NoSuchTicket(eventId, eventTicketId, eventBookingTicketId);

            var eventBookingTicketValidation = _eventRepository.GetEventBookingTicketValidation(eventBookingTicketId);
            if (eventBookingTicketValidation != null)
            {
                eventBookingTicketValidation.IncrementCount();
                _eventRepository.UpdateEventBookingTicketValidation(eventBookingTicketValidation);
                return EventBookingTicketValidationResult.PartialSuccess();
            }

            // CreateRepository the validation for the first time
            _eventRepository.CreateEventBookingTicketValidation(new EventBookingTicketValidation(eventBookingTicketId));
            return EventBookingTicketValidationResult.Success(eventModel, eventBookingTicket);
        }
    }
}