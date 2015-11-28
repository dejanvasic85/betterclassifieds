namespace Paramount.Common.UIController.ViewObjects
{
    using System;
    using System.Collections.ObjectModel;

    public class EventScheduleView
    {
        public int EventScheduleId { get; set; }

        public string Location { get; set; }

        public int RegionId { get; set; }

        public DateTime ScheduleDateTime { get; set; }

        public string Comments { get; set; }

        public Collection<EventScheduleSeatingView> Seatings { get; set; }

        public EventScheduleView()
        {
            Seatings = new Collection<EventScheduleSeatingView>();
        }
    }
}
