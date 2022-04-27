using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface IStateService
    {
        List<State> GetAll(string name);

    }
}