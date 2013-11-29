namespace Paramount.DomainModel.Business.OnlineClassies
{
    public interface IGalleryAd 
    {
        string ImageUrl { get; } 
        string Title { get;}
        string Description { get; set; }
        double Price { get;  }
    }
    

    public class GalleryAd : IGalleryAd
    {
        public string ImageUrl { get; private set; }
        public string Title { get; private set; }
        public string Description { get; set; }
        public double Price { get; private set; }

        public GalleryAd(string title, string description, string imageUrl, double price)
        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
        }

        public GalleryAd()
        {
            
        }
    }
}