namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IPdfGenerator
    {
        byte[] BuildFromHtml(string html);
    }

    public class PdfGenerator : IPdfGenerator
    {
        public byte[] BuildFromHtml(string html)
        {
            return new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(html);
        }
    }
}