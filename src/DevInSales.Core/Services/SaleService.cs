using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Services
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

        public List<Sale> GetSellerById(int? userId)
        {
            return _context.Sales.Where(p => p.SellerId == userId).ToList();
        }
    }
}