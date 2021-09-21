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

        Task<WorkingState> WorkingState(ILocalStorageService localStorageService);
    }
}