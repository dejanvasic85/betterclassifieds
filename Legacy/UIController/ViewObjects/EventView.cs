namespace Paramount.Common.UIController.ViewObjects
{
    using System;
    using System.Collections.ObjectModel;

    public class EventView
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public Guid? EventId { get; set; }

        public Collection<EventScheduleView> ScheduleList { get; set; }

        public bool StandardEvent { get; set; }

        public EventView()
        {
            ScheduleList = new Collection<EventScheduleView>();
        }
    }
}
