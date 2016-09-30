namespace Paramount.Betterclassifieds.Business
{
    public interface IBarcodeGenerator
    {
        byte[] CreateQr(string barcodeData, int height, int width, int margin = 0);
    }
}
