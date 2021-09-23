using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace pointeuse
{
    public interface ITimeClock
    {
        Task Start(ILocalStorageService localStorageService);

        Task Stop(ILocalStorageService localStorageService);

        Task<IEnumerable<IGrouping<string,DayTime>>> GetHistoric(ILocalStorageService localStorageService);

        Task<WorkingState> WorkingState(ILocalStorageService localStorageService);
    }
}