using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Entities
{
    public class Delivery : Entity
    {
        public int AdressId { get; private set; }
        public int SaleId { get; private set; }
        public DateTime DeliveryForecast { get; private set; }

        public Delivery(int adressId, int saleId, DateTime deliveryForecast)
        {
            AdressId = adressId;
            SaleId = saleId;
            DeliveryForecast = deliveryForecast;
        }        
        public Sale? Sale { get; private set; }  
        public Address? Address { get; private set; }        
    }
}
