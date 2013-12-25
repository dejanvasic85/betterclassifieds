namespace BetterClassified.Repository
{
    using System.Collections.Generic;
    using Models;


    public interface IUserRepository
    {
        ApplicationUser GetClassifiedUser(string username);
    }
}