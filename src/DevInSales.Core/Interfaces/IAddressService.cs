using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface IAddressService
    {
        List<ReadAddress> GetAll(int? stateId, int? cityId, string? street, string? cep);
        Address? GetById(int addressId);
        void Add(Address address);
        void Delete(Address address);
    }
}