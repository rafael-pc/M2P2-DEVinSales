using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Interfaces
{
    public interface ICityService
    {
        List<ReadCity> GetById(int stateId, string? name);
    }
}