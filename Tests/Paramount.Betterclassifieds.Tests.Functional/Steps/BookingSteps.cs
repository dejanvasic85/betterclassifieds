using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingSteps : BaseStep
    {
        private readonly ITestDataManager _dataManager;

        public BookingSteps(IWebDriver driver, IConfig configuration, ITestDataManager dataManager)
            : base(driver, configuration)
        {
            _dataManager = dataManager;
        }

        

    }
}
