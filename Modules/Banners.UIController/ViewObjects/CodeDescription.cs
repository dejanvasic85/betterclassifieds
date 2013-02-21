namespace Paramount.Banners.UIController.ViewObjects
{
    public class CodeDescription
    {
        public CodeDescription() { }
        public CodeDescription(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
