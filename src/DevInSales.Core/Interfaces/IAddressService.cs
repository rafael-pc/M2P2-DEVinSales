using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface IAddressService
    {
        ReadAddress GetById(int addressId);
        List<ReadAddress> GetAll(int? stateId, int? cityId, string? street, string? cep);
        void Add(Address address);
        void UpdateStreet(Address address, string street);
        void UpdateCep(Address address, string cep);
        void UpdateNumber(Address address, int number);
        void UpdateComplement(Address address, string complement);
    }
}