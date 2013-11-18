using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class RegistrationSteps : BaseStep
    {
        private readonly Mocks.ITestDataManager dataManager;
        private readonly TestRouter testRouter;

        public RegistrationSteps(IWebDriver webDriver, IConfig configuration, Mocks.ITestDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            this.dataManager = dataManager;
            this.testRouter = testRouter;
        }

    }
}
