using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface IStateService
    {
        List<State> GetAll(string name);
        State GetByStateId(int stateId);

        List<City> GetCityByStateId(int stateId, string name);
    }
}