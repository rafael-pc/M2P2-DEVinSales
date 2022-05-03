using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface IDeliveryService
    {
        public List<Delivery> GetBy(int? idAddress, int? saleId);                
    }
}