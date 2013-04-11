using Microsoft.Practices.Unity;

namespace BetterClassified.UI
{


    public abstract class BaseCompositeControl<TController, TView> : System.Web.UI.WebControls.CompositeControl, IBaseView where TView : IBaseView
    {
        protected TController Controller;

        protected BaseCompositeControl()
        {
            this.Init += (sender, args) => this.Controller = Unity.DefaultContainer.Resolve<TController>(With.Parameter("view", this));
        }

        public void PrintMessage()
        {
            // Find master and print the message
        }

        public void PrintError()
        {
            // Find master and print the error
        }

        public void NavigateToHome()
        {
            // Navigate to home page (iflog home page)
        }
    }
}
