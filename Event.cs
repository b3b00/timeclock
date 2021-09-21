using System;

namespace pointeuse
{
    public class Event
    {
        public Event(EventType type)
        {
            Date = DateTime.Now;
            Type = type;
        }
        
        public DateTime Date { get; set; }

        public EventType Type { get; set; }
    }
}