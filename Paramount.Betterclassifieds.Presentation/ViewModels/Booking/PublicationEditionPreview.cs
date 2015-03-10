namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PublicationNameAndEditionListView
    {
        public string Publication { get; set; }
        
        // We'll convert the date format in the controller
        // Because default json serialiser is just crazy.
        // When we move to WebAPI we should be using JSON.net serialiser because it 'fixes' the situation
        // See http://www.hanselman.com/blog/OnTheNightmareThatIsJSONDatesPlusJSONNETAndASPNETWebAPI.aspx
        public string[] Editions { get; set; } 
    }
}