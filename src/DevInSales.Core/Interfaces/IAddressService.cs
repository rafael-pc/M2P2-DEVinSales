using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Interfaces
{
    public interface IAddressService
    {
        List<ReadAddress> GetAll(int? stateId, int? cityId, string? street, string? cep);
    }
}