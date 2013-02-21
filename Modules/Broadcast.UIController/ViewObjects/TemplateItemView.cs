namespace Paramount.Broadcast.UIController.ViewObjects
{
    public class TemplateItemView
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public TemplateItemView()
        {

        }
        public TemplateItemView(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
