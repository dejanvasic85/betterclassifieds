using System;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    /// <summary>
    /// Turns the bootstrap toggle/checkbox button on and off and hides all the complexity
    /// </summary>
    public class ToggleButton
    {
        public string EnabledText { get; private set; }
        public string DisabledText { get; private set; }
        public IWebElement ParentElement { get; private set; }


        public ToggleButton(IWebElement parentElement)
            : this(parentElement, "Yes Please", "No Thanks")
        { }

        public ToggleButton(IWebElement parentElement, string enabledText, string disabledText)
        {
            ParentElement = parentElement;
            EnabledText = enabledText;
            DisabledText = disabledText;
        }

        public void TurnOn()
        {
            if (!IsCurrentlyEnabled())
            {
                ParentElement.ClickOnElement();
            }
        }

        public void TurnOff()
        {
            if (IsCurrentlyEnabled())
            {
                ParentElement.ClickOnElement();
            }
        }

        private bool IsCurrentlyEnabled()
        {
            var childElement = ParentElement.FindElement(By.ClassName("toggle"));

            return !childElement.HasClass("off");

            //if (childElement == null)
            //{
            //    throw new NoSuchElementException("Toggle does not have an active child element");
            //}
            //var currentText = childElement.Text;
            //if (currentText.Equals(EnabledText, StringComparison.OrdinalIgnoreCase))
            //{
            //    return true;
            //}

            //if (currentText.Equals(DisabledText, StringComparison.OrdinalIgnoreCase))
            //{
            //    return false;
            //}

            //throw new Exception("Toggle button does not have configured Enabled and Disabled text");
        }
    }
}