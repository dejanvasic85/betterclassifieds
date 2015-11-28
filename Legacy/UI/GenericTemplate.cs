namespace Paramount.Common.UI
{
    using System.Web.UI;

    public class GenericTemplate<T>:ITemplate where T:Control, new()
    {
        public void InstantiateIn(Control container)
        {
            var control = new T();
            container.Controls.Add(control);
        }
    }
}
