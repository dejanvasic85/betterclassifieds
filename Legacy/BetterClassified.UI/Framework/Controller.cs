namespace BetterClassified
{
    public class Controller<TView> where TView : IBaseView
    {
        protected TView View { get; set; }

        public Controller(TView view)
        {
            this.View = view;
        }
    }
}