using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventPromoService
    {
        IEnumerable<EventPromoCode> GetEventPromoCodes(int eventId);

        EventPromoCode GetEventPromoCode(int eventId, string promoCode);

        EventPromoCode CreateEventPromoCode(int eventId, string promoCode, decimal? discountPercent);

        void DisablePromoCode(long eventPromoCodeId);
    }

    public class EventPromoService : IEventPromoService
    {
        private readonly IEventRepository _repository;

        public EventPromoService(IEventRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<EventPromoCode> GetEventPromoCodes(int eventId)
        {
            return _repository.GetEventPromoCodes(eventId).Where(p => !p.IsDisabled.GetValueOrDefault());
        }

        public EventPromoCode GetEventPromoCode(int eventId, string promoCode)
        {
            var promo = _repository.GetEventPromoCode(eventId, promoCode);

            if (promo == null || promo.IsDisabled.GetValueOrDefault())
            {
                return null;
            }

            return promo;
        }

        public EventPromoCode CreateEventPromoCode(int eventId, string promoCode, decimal? discountPercent)
        {
            var eventPromoCode = EventPromoCodeFactory.Create(eventId, promoCode, discountPercent);

            _repository.CreateEventPromoCode(eventPromoCode);

            return eventPromoCode;
        }

        public void DisablePromoCode(long eventPromoCodeId)
        {
            var promo = _repository.GetEventPromoCode(eventPromoCodeId);
            promo.IsDisabled = true;

            _repository.UpdateEventPromoCode(promo);
        }
    }
}