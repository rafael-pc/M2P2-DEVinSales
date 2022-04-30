using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface ISaleService
    {
        //GetSale por Id
        public Sale? GetSaleById(int id);
        //GetSale por userId
        public List<Sale> GetSaleByUserId(int userId);
        //PostSale
        public int CreateSale(Sale sale);
        //Patch
        //Aguardando...        
    }
}