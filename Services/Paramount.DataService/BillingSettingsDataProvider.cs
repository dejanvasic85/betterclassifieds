using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.ApplicationBlock.Data;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.DataService.LinqObjects;

namespace Paramount.DataService
{
    public class BillingSettingsDataProvider : IDisposable
    {
        private BillingDatabaseModelDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";

        public BillingSettingsDataProvider(string clientCode)
        {
            _context = new BillingDatabaseModelDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public string ClientCode { get; private set; }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public IQueryable<BillingSetting> AsRawQueryable()
        {
            return _context.BillingSettings.Where(a => a.ClientCode == ClientCode);
        }

        public BillingSettingsEntity GetBillingSettings()
        {
            var billingSetting = AsRawQueryable().FirstOrDefault(item => item.ClientCode == ClientCode);
            return billingSetting.Convert();
        }

        public BillingBankEntity GetBankDetails(Guid bankId)
        {
            return _context.Billing_Banks.FirstOrDefault(a => a.BankId == bankId).Convert();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }

        public bool SaveSettings(BillingSettingsEntity billingSettingsEntity)
        {
            billingSettingsEntity.ClientCode = ClientCode;
            var billingSettings = AsRawQueryable().FirstOrDefault(item => item.ClientCode == ClientCode);
            if (billingSettings == null)
            {

                _context.BillingSettings.InsertOnSubmit(billingSettingsEntity.Convert());
            }
            else
            {
                billingSettings.BankId = billingSettingsEntity.BankId;
                billingSettings.BankName = billingSettingsEntity.BankName;
                billingSettings.ClientCode = billingSettingsEntity.ClientCode;
                billingSettings.CollectAddressDetails = billingSettingsEntity.CollectAddressDetails;
                billingSettings.Description = billingSettingsEntity.Description;
                billingSettings.GatewayUrl = billingSettingsEntity.GatewayUrl;
                billingSettings.GSTRate = billingSettingsEntity.GSTRate;
                billingSettings.InvoiceBannerImageId = billingSettingsEntity.InvoiceBannerImageId;
                billingSettings.PP_BusinessEmail = billingSettingsEntity.PaypalBusinessEmail;
                billingSettings.ReferencePrefix = billingSettingsEntity.ReferencePrefix;
                billingSettings.RefundPolicyUrl = billingSettingsEntity.RefundPolicyUrl;
                billingSettings.ReturnLinkText = billingSettingsEntity.ReturnLinkText;
                billingSettings.VendorCode = billingSettingsEntity.VendorCode;
                billingSettings.AlertEmail = billingSettingsEntity.PaymentAlertEmail;
                billingSettings.RefundPolicyUrl = billingSettingsEntity.RefundPolicyUrl;

            }

            return true;
        }

        public bool SaveBankSettings(BillingBankEntity billingBank)
        {
            _context.Billing_Banks.InsertOnSubmit(billingBank.Convert());
            return true;
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }
    }
}
