using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Paramount.Broadcast.Components
{
    public class SystemHealthCheckNotification : Email
    {
        private readonly int _totalBookings;
        private readonly decimal _totalIncome;
        private readonly int _numberOfEmailsSent;
        private readonly int _topOneErrorOccurances;
        private readonly string _topOneErrorApplicationName;
        private readonly string _topOneErrorMessage;
        private readonly Collection<string> _recipients;

        public SystemHealthCheckNotification(int totalBookings, decimal totalIncome, int emailCount,
            int topOneErrorCount, string topOneErrorApplication, string topOneErrorMessage, Collection<string> recipients)
        {
            this._totalBookings = totalBookings;
            this._totalIncome = totalIncome;
            this._numberOfEmailsSent = emailCount;
            this._topOneErrorOccurances = topOneErrorCount;
            this._topOneErrorApplicationName = topOneErrorApplication;
            this._topOneErrorMessage = topOneErrorMessage;
            this._recipients = recipients;
        }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                return new Collection<TemplateItemView>
                    {
                        new TemplateItemView("TotalBookings", _totalBookings.ToString()),
                        new TemplateItemView("TotalIncome", _totalIncome.ToString("N")),
                        new TemplateItemView("TotalEmailsSent", _numberOfEmailsSent.ToString()),
                        new TemplateItemView("TopErrorOccurrances", _topOneErrorOccurances.ToString()),
                        new TemplateItemView("TopErrorApplicationName", _topOneErrorApplicationName),
                        new TemplateItemView("TopErrorMessage", _topOneErrorMessage)
                    };
            }
        }

        public override string EmailTemplateName
        {
            get { return "SystemHealthCheck"; }
        }

        public override string Subject
        {
            get { return "Nightly System Health Check"; }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get
            {
                Collection<EmailRecipientView> emails = new Collection<EmailRecipientView>();
                foreach (var item in _recipients)
                {
                    emails.Add(new EmailRecipientView() {Email = item});
                }
                return emails;
            }
        }
    }
}