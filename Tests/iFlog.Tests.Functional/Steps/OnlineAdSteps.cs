using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace iFlog.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps : BaseStep
    {
        private Pages.OnlineAdPage _onlineAdPage;
        private Router _router;

        public OnlineAdSteps(IWebDriver webDriver) : base(webDriver)
        {
            _onlineAdPage = new Pages.OnlineAdPage(webDriver);
        }

        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(string adTitle)
        {
            // todo - setup mock data
        }

        [When(@"I navigate to ad URL for ""(.*)""")]
        public void WhenINavigateToAdURLFor(string adTitle)
        {
            // Open selenium
            Router.NavigateTo(_onlineAdPage);
        }

        [Then(@"the page should display tutor ad information")]
        public void ThenThePageShouldDisplayTutorAdInformation()
        {
            // todo - assert
        }

    }
}
