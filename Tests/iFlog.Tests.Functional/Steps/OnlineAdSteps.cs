using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace iFlog.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps
    {
        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(string adTitle)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I navigate to ad URL for ""(.*)""")]
        public void WhenINavigateToAdURLFor(string adTitle)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the page should display tutor ad information")]
        public void ThenThePageShouldDisplayTutorAdInformation()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
