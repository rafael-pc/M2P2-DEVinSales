using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public SaleResponse GetSaleById(int id)
        {
            Sale? sale = _context.Sales
                .Include(p => p.Buyer)
                .Include(p => p.Seller)
                .FirstOrDefault(p => p.Id == id);

            if (sale == null)
            {
                return null;
            }

            var listaProdutos = GetSaleProductsBySaleId(id);

            return new SaleResponse(sale.Id, sale.Seller.Name, sale.Buyer.Name, sale.SaleDate, listaProdutos);
        }

        public List<SaleProductResponse> GetSaleProductsBySaleId(int id)
        {
            return _context.SaleProducts
                .Where(p => p.SaleId == id)
                .Include(p => p.Products)
                .Select(p => new SaleProductResponse(p.Products.Name, p.Amount, p.UnitPrice, p.Amount * p.UnitPrice))
                .ToList();
        }
    }
}