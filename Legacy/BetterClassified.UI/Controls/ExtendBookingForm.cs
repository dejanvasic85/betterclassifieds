//using System;
//using System.Collections.Generic;
//using System.Web;
//using System.Web.UI.WebControls;
//using BetterClassified.UIController;
//using BetterClassified.UIController.ViewObjects;
//using BetterClassified.UIController.Views;
//using Telerik.Web.UI;

//namespace BetterClassified.UI
//{
//    /// <summary>
//    /// Form control used for extending an existing booking by allowing user to specify additional insertion number
//    /// </summary>
//    public class ExtendBookingForm : BaseCompositeControl<ExtendBookingController, IExtendBookingView>, IExtendBookingView
//    {
//        private readonly Button btnSubmit;
//        private readonly FormDropDownList ddlInsertions;
//        private readonly FormLabel pricePerEditionLabel;
//        private readonly FormLabel totalPriceLabel;
//        private readonly CheckBox chkAgreeToTerms;
//        private readonly Repeater editionRepeater;

//        public ExtendBookingForm()
//        {
//            ddlInsertions = new FormDropDownList();
//            ddlInsertions.IndexChanged += InsertionsSelectionChanged;
//            btnSubmit = new Button { Text = "Submit" };
//            btnSubmit.Click += SubmitClick;
//            pricePerEditionLabel = new FormLabel { Text = "Price Per Insertion", HelpText = "Help text" };
//            totalPriceLabel = new FormLabel { Text = "Total Extension Price" };
//            chkAgreeToTerms = new CheckBox { Text = "I agree to the terms and conditions" };
//            editionRepeater = new Repeater();
//            editionRepeater.ItemTemplate = new EditionViewTemplate();
//        }

//        private void SubmitClick(object sender, EventArgs eventArgs)
//        {
//            Controller.ProcessExtensions();
//        }

//        protected void InsertionsSelectionChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs eventArgs)
//        {
//            // Handle the event change here
//            Controller.LoadForInsertions(int.Parse(eventArgs.Value));
//        }

//        protected override void CreateChildControls()
//        {
//            this.Controller.Load();

//            this.Controls.Add(ddlInsertions);
//            this.Controls.Add(pricePerEditionLabel);
//            this.Controls.Add(totalPriceLabel);
//            this.Controls.Add(chkAgreeToTerms);
//            this.Controls.Add(btnSubmit);

//            this.Controls.Add(editionRepeater);
//        }

//        public int AdBookingId
//        {
//            get { return HttpContext.Current.ReadQueryString<int>("AdBookingId"); }
//        }

//        public void DataBindInsertionList(IEnumerable<int> insertions)
//        {
//            this.ddlInsertions.DataSource = insertions;
//            this.ddlInsertions.DataBind();
//        }

//        public bool IsPaymentRequired { get; set; }

//        public int SelectedInsertionCount
//        {
//            get { return ddlInsertions.SelectedValue.ToInt().GetValueOrDefault(); }
//        }

//        public IEnumerable<PublicationEditionModel> Editions
//        {
//            set
//            {
//                editionRepeater.DataSource = value;
//                editionRepeater.DataBind();
//            }
//        }
//    }
//}
