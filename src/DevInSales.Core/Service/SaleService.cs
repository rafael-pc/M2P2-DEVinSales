using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Service
{
    public class SaleService : ISaleService
    {
        private readonly DataContext _context;

        public SaleService(DataContext context)
        {
            _context = context;
        }
        public int CreateSale(Sale sale)
        {
            throw new NotImplementedException();
        }

        public Sale? GetSaleById(int id)
        {
            throw new NotImplementedException();
        }
        
        public List<Sale> GetSaleByUserId(int userId)
        {
            return _context.Sales
            .Where(p => p.BuyerId == userId)
            .Include(p => p.Seller)
            .Include(p => p.Buyer)
            .ToList();
        }
    }
}