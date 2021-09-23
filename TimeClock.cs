using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace pointeuse
{
    public class TimeClock : ITimeClock
    {
        private  List<Event> Events { get; set; }
        
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        public TimeClock()
        {
            
        }

        public async Task<WorkingState> WorkingState(ILocalStorageService localStorageService)
        {
            await InitIfNeeded(localStorageService);
            if (Events.Any())
            {
                return Events.Last().Type == EventType.Start ? pointeuse.WorkingState.Working : pointeuse.WorkingState.Idle;
            }
            return pointeuse.WorkingState.Idle;
        }
        
        private async Task Load()
        {
            var times = await LocalStorage.GetItemAsync<List<Event>>("times");
            var tstr = await LocalStorage.GetItemAsStringAsync("times");
            
            Events = times;
        }

        private async Task Save()
        {
            await LocalStorage.SetItemAsync<List<Event>>("times", Events);
        }
        
        public async Task Start(ILocalStorageService localStorageService)
        {
            await InitIfNeeded(localStorageService);
            var evt = new Event(EventType.Start);
            Events.Add(evt);
            await Save();
        }

        private async Task InitIfNeeded(ILocalStorageService localStorageService)
        {
            LocalStorage = localStorageService;
            if (Events == null)
            {
                await Load();
                if (Events == null)
                {
                    Events = new List<Event>();
                }
            }
        }

        public async Task Stop(ILocalStorageService localStorageService)
        {
            await InitIfNeeded(localStorageService);
            var evt = new Event(EventType.Stop);
            Events.Add(evt);
            await Save();
        }

        public async Task<IEnumerable<IGrouping<string,DayTime>>> GetHistoric(ILocalStorageService localStorageService)
        {
            var days = new List<DayTime>();
            await InitIfNeeded(localStorageService);

            var byDay = Events.GroupBy(x => x.Date.Day + "/" + x.Date.Month + "/" + x.Date.Year).ToList();
            foreach (var dayEvents in byDay)
            {
                if (dayEvents.Count() % 2 == 0)
                {
                    for (int i = 0; i < dayEvents.Count()/2; i++)
                    {
                        var start = dayEvents.ElementAt(i * 2);
                        var stop = dayEvents.ElementAt(i * 2 + 1);
                        var day = new DayTime()
                        {
                            Start = start.Date,
                            Stop = stop.Date
                        };
                        days.Add(day);
                    }
                }
            }
            
            return days.GroupBy(x => x.Day);
        }
    }
}