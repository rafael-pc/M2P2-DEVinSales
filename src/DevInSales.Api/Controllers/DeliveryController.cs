using Microsoft.AspNetCore.Mvc;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("/deliver")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }
        [HttpGet]
        public ActionResult<Delivery> GetDelivery(int? idAddress, int? saleId)
        {
            var delivery = _deliveryService.GetBy(idAddress, saleId);
            if (delivery.Count == 0)
                return NoContent();
            return Ok(delivery);
        }
    }
}