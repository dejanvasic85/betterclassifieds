using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingSteps
    {
        private readonly ITestDataManager _dataManager;

        public BookingSteps(ITestDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Given(@"The category ""(.*)"" and publication ""(.*)"" is a free category for online and line ads")]
        public void GivenTheCategoryAndPublicationIsAFreeCategoryForOnlineAndLineAds(string categoryName, string publicationName)
        {
            // todo - setup rates

            ScenarioContext.Current.Pending();
        }

        [Given(@"the publication ""(.*)"" has at least (.*) editions")]
        public void GivenThePublicationHasAtLeastEditions(string publicatioName, int numberofEditions)
        {
            // todo setup the editions for publication

            ScenarioContext.Current.Pending();
        }

        

    }
}
