using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface ISaleService
    {
        public SaleResponse GetSaleById(int id);
        
        public int CreateSaleByUserId(Sale sale);
        
        public List<Sale> GetSaleBySellerId(int? userId);

        public List<Sale> GetSaleByBuyerId(int? userId);
    }
}