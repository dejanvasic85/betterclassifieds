namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class DirectDebitViewModel
    {
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string BSB { get; set; }
        public string AccountNumber { get; set; }

        public bool IsConfigured()
        {
            return BankName.HasValue()
                   && AccountName.HasValue()
                   && BSB.HasValue()
                   && AccountNumber.HasValue();
        }
    }
}