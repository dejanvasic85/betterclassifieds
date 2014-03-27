using System.Data;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class BroadcastRepository : IBroadcastRepository
    {
        public EmailTemplate GetTemplateByName(string templateName)
        {
            using (var context = new BroadcastContext())
            {
                return context.EmailTemplates.FirstOrDefault(t => t.DocType.Equals(templateName));
            }
        }

        public int CreateOrUpdateEmail(EmailDelivery emailDelivery)
        {
            using (var context = new BroadcastContext())
            {
                if (emailDelivery.EmailDeliveryId != default(int))
                {
                    // Create
                    context.Emails.Add(emailDelivery);
                }
                else
                {
                    // Update
                    var oldValue = context.Emails.Single(e => e.EmailDeliveryId == emailDelivery.EmailDeliveryId);
                    context.Entry(oldValue).CurrentValues.SetValues(emailDelivery);
                    context.Entry(oldValue).State = EntityState.Modified;
                }

                context.SaveChanges();

                return emailDelivery.EmailDeliveryId;
            }
        }
    }
}
