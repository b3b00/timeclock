using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace pointeuse
{
    public interface ITimeClock
    {
        Task Start(ILocalStorageService localStorageService);

        Task Stop(ILocalStorageService localStorageService);

        Task<List<DayTime>> GetHistoric(ILocalStorageService localStorageService);
        
    }

    public enum EventType
    {
        Start,
        Stop
    }
    
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

    public class DayTime
    {
        public DateTime Start { get; set; }
        
        public DateTime Stop { get; set; }

        public TimeSpan Duration => Stop - Start;

        public string Day => Start.ToString("dddd d/M/yyyy");

    }
}