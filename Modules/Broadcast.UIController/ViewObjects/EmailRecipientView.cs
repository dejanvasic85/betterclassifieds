namespace Paramount.Broadcast.UIController.ViewObjects
{
    using System.Collections.ObjectModel;

    public class EmailRecipientView
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Collection<TemplateItemView> TemplateFields { get; set; }
        public EmailRecipientView()
        {
            TemplateFields = new Collection<TemplateItemView>();
        }
    }
}
