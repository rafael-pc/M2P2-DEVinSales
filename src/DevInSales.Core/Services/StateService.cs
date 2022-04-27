using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Services
{
    public class StateService : IStateService
    {
        private readonly DataContext _context;

        public StateService(DataContext context)
        {
            _context = context;
        }

        public List<State> GetAll(string name)
        {
            return _context.States
                .Where(p => !String.IsNullOrWhiteSpace(name) ? p.Name.ToUpper().Contains(name.ToUpper()) : true)
                .ToList();
        }

        public State GetById(int id)
        {
            return _context.States
                .SingleOrDefault(p => p.Id == id);
        }
    }
}