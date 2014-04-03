//namespace Paramount.Broadcast.Components
//{
//    using System.Collections.ObjectModel;

//    public class EmailRecipientView
//    {
//        public string Name { get; set; }

//        public string Email { get; set; }

//        public Collection<TemplateItemView> TemplateFields { get; set; }
//        public EmailRecipientView()
//        {
//            TemplateFields = new Collection<TemplateItemView>();
//        }

//        public EmailRecipientView AddTemplateItem(string name, string value)
//        {
//            if (TemplateFields == null)
//                TemplateFields = new Collection<TemplateItemView>();
//            TemplateFields.Add(new TemplateItemView(name, value));
//            return this;
//        }
//    }
//}
