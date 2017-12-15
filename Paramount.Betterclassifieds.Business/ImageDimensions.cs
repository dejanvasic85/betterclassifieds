namespace Paramount.Betterclassifieds.Business
{
    public struct ImageDimensions
    {
        public ImageDimensions(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height { get; }
        public int Width { get; }        
    }
}