using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Services
{
  public class AddressService : IAddressService
  {
    private readonly DataContext _context;

    public AddressService(DataContext context)
    {
      _context = context;
    }

    public ReadAddress GetById(int addressId)
    {
      var address = _context.Addresses
          .Include(a => a.City)
          .Include(a => a.City.State)
          .FirstOrDefault(p => p.Id == addressId);

      return ReadAddress.AddressToReadAddress(address);
    }
    public List<ReadAddress> GetAll(int? stateId, int? cityId, string? street, string? cep)
    {
      var query = _context.Addresses
          .Include(a => a.City)
          .Include(a => a.City.State)
          .AsQueryable();
      if (cityId.HasValue)
        query = query.Where(x => x.CityId == cityId);
      if (stateId.HasValue)
        query = query.Where(x => x.City.StateId == stateId);
      if (!string.IsNullOrEmpty(street))
        query = query.Where(x => x.Street.ToUpper().Contains(street.ToUpper()));
      if (!string.IsNullOrEmpty(cep))
        query = query.Where(x => x.Cep == cep);
      return query.Select(x => ReadAddress.AddressToReadAddress(x)).ToList();
    }

    public void Add(Address address)
    {
      _context.Addresses.Add(address);
      _context.SaveChanges();
    }
    public void UpdateStreet(Address address, string street)
    {
      address.UpdateStreet(street);
      _context.SaveChanges();
    }
    public void UpdateCep(Address address, string cep)
    {
      address.UpdateCep(cep);
      _context.SaveChanges();
    }
    public void UpdateNumber(Address address, int number)
    {
      address.UpdateNumber(number);
      _context.SaveChanges();
    }
    public void UpdateComplement(Address address, string complement)
    {
      address.UpdateStreet(complement);
      _context.SaveChanges();
    }
  }
}
