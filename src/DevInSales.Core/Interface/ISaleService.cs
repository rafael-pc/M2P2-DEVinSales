using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface ISaleService
    {
        //GetSale por Id
        public Sale? GetSaleById(int id);
        //PostSale
        public int CreateSale(Sale sale);
        //Patch
        //Aguardando...        
    }
}