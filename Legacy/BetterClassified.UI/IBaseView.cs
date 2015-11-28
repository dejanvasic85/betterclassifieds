namespace BetterClassified
{
    public interface IBaseView
    {
        void NavigateToHome();
        string LoggedInUserName { get; }
    }
}