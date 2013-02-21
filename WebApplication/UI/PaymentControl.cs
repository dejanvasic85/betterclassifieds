namespace Paramount.Common.UI
{
    using BaseControls;
    using System.Web.UI.WebControls;

    public class PaymentControl : ParamountCompositeControl
    {
        private readonly CheckBox _paymentTypeControl;

        private readonly TextBox _nameBox;
        private readonly ParamountLabel _nameLabel;

        private readonly ParamountLabel _credicCardNumberBox;
        private readonly ParamountLabel _creditCardNumberLabel;
    }
}
