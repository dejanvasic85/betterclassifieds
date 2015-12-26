using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    internal class FieldDefinition
    {
        private readonly IWebElement _parentElement;

        public FieldDefinition(IWebElement parentElement)
        {
            _parentElement = parentElement;
        }

        public void SetField(string fieldName, bool isRequired)
        {
            _parentElement.FindElement(By.ClassName("js-field-name")).FillText(fieldName);
            var toggle = new ToggleButton(_parentElement.FindElement(By.ClassName("toggle-required")), 
                enabledText:"Yes",
                disabledText:"No");
            toggle.Toggle(isRequired);
        }

        public void Remove()
        {
            _parentElement.FindElement(By.ClassName("remove-field")).Click();
        }
    }
}