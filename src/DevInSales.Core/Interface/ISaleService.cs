using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface ISaleService
    {
        public SaleResponse GetSaleById(int id);
        public int CreateSale(Sale sale);
        public List<Sale> GetSellerById(int? userId);     
    }
}