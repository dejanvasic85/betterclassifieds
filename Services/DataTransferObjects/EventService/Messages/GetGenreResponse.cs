namespace Paramount.Common.DataTransferObjects.EventService.Messages
{
    using System.Collections.ObjectModel;

    public class GetGenreResponse
    {
        public Collection<ListItem> GenreList { get; set; }

        public GetGenreResponse()
        {
            GenreList = new Collection<ListItem>();
        }
    }
}
