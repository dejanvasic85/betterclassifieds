using System.Threading;
using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Components
{
    /// <summary>
    /// Turns the bootstrap toggle/checkbox button on and off and hides all the complexity
    /// </summary>
    internal class ToggleButton
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
                Thread.Sleep(800); // Wait for animation
            }
        }

        public void TurnOff()
        {
            if (IsCurrentlyEnabled())
            {
                ParentElement.ClickOnElement();
                Thread.Sleep(800); // Wait for animation
            }
        }

        private bool IsCurrentlyEnabled()
        {
            var childElement = ParentElement.FindElement(By.ClassName("toggle"));

            return !childElement.HasClass("off");
        }

        public void Toggle(bool required)
        {
            if (required)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
    }
}