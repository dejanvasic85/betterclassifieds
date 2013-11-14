using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class RegistrationSteps : BaseStep
    {
        private readonly Mocks.IDataManager dataManager;
        private readonly TestRouter testRouter;

        public RegistrationSteps(IWebDriver webDriver, IConfig configuration, Mocks.IDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            this.dataManager = dataManager;
            this.testRouter = testRouter;
        }

    }
}
