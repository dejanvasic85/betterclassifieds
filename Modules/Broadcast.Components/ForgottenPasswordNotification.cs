namespace Paramount.Broadcast.Components
{
    using System.Collections.ObjectModel;

    public class ForgottenPasswordNotification : Email
    {

        private readonly string _username;
        private readonly string _password;
        private readonly string _email;

        public ForgottenPasswordNotification(string username, string password, string email)
        {
            _username = username;
            _password = password;
            _email = email;
        }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                var list = new Collection<TemplateItemView>
                               {
                                   new TemplateItemView ("username",  _username),
                                   new TemplateItemView ("password",  _password),
                                   new TemplateItemView ( "email", _email)
                               };

                return list;
            }
        }

        public override string EmailTemplateName
        {
            get { return "ForgottenPassword"; }
        }

        public override string Subject
        {
            get { return "Password Recovery"; }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get
            {
                return new Collection<EmailRecipientView> { new EmailRecipientView { Email = _email, Name = _username } };
            }
        }
    }
}
