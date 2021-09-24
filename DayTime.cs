using System;

namespace pointeuse
{
    public class DayTime
    {
        public DateTime Start { get; set; }
        
        public DateTime Stop { get; set; }

        public TimeSpan Duration => Stop - Start;

        public string Day => Start.ToString("dddd d/M/yyyy");

        public string StartTime => Start.ToString("HH:mm");
        
        public string StopTime => Stop.ToString("HH:mm");

    }
}