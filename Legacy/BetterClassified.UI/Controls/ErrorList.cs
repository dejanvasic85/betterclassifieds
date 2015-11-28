using System;
using System.Collections.Generic;
using Paramount.Common.UI.BaseControls;
using System.Web.UI.WebControls;
using System.Collections;
namespace BetterClassified.UI
{
    public class ErrorList : ParamountCompositeControl
    {
        private Panel _panelContainer;
        private Panel _panelErrorList;
        private BulletedList _bulletedList;
        private Label _labelMessage;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _panelContainer = new Panel { CssClass = "errorlist-panelcontainer", Visible = false };
            _panelErrorList = new Panel { CssClass = "errorlist-panelerrorlist" };
            _labelMessage = new Label { Text = "Please correct the following:", CssClass = "errorlist-labelmessage" };
            _bulletedList = new BulletedList();

            // Add the controls to the UI accordingly
            _panelErrorList.Controls.Add(_labelMessage);
            _panelErrorList.Controls.Add(_bulletedList);
            _panelContainer.Controls.Add(_panelErrorList);
            Controls.Add(_panelContainer);
        }

        public void DisplayErrors(List<String> errorList)
        {
            if (errorList.Count > 0)
            {
                _panelContainer.Visible = true;
                // DataBind the list of errors
                _bulletedList.DataSource = errorList;
                _bulletedList.DataBind();
            }
        }

        public void HideErrors()
        {
            _panelContainer.Visible = false;
        }
    }
}