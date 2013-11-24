using System.Collections.Generic;
using Paramount.Betterclassifieds.Domain.Notifications;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataManager
    {
        ITestDataManager Initialise();
        ITestDataManager AddOrUpdateOnlineAd(string adTitle, out int? id);
        ITestDataManager DropUserIfExists(string username);
        Domain.Membership.User GetUserProfile(string username);
        List<Email> GetSentEmailsFor(string email);
    }
}
