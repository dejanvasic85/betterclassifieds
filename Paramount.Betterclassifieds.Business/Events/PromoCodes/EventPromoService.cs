using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventPromoService
    {
        IEnumerable<EventPromoCode> GetEventPromoCodes(int eventId);

        EventPromoCode GetEventPromoCode(int eventId, string promoCode);
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
    }
}