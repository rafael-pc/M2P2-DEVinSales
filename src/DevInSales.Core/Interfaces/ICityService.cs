using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Interfaces
{
    public interface ICityService
    {
        List<ReadCity> GetAll(int stateId, string? name);
        ReadCity GetById(int cityId);
    }
}