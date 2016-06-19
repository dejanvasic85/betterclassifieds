using System;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Console.Tasks.EventDailySummary
{
    public class EventDailySummary : ITask
    {
        private readonly IEventRepository _eventRepository;

        public EventDailySummary(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void HandleArgs(TaskArguments args)
        {
            args.ReadArgument("Date");
        }

        public void Run()
        {
            
        }

        public bool Singleton { get; }
    }
}
