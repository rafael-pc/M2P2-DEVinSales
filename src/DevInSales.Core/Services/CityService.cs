using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Interfaces;

namespace DevInSales.Core.Services
{
    public class CityService : ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext context)
        {
            _context = context;
        }

        public List<ReadCity> GetById(int stateId, string? name)
        {
            return _context.Cities
                .Where(
                    p =>
                        p.StateId == stateId
                        && (
                            !String.IsNullOrEmpty(name)
                                ? p.Name.ToUpper().Contains(name.ToUpper())
                                : true
                        )
                )
                .Select(c => ReadCity.CityToReadCity(c))
                .ToList();
        }
    }
}
