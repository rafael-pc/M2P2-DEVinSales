using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface ISaleService
    {
        //GetSale por Id
        public SaleResponse GetSaleById(int id);
        //PostSale
        public int CreateSale(Sale sale);
        //Patch
        //Aguardando...        
    }
}