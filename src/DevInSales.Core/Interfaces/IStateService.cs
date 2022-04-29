using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Interfaces
{
    public interface IStateService
    {
        List<ReadState> GetAll(string? name);
        ReadState GetById(int stateId);
    }
}