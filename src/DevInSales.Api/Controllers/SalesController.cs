
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("api/sales/")]

    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("{saleId}")]
        public ActionResult<SaleResponse> GetSaleById(int saleId)
        {
            var sale = _saleService.GetSaleById(saleId);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpGet("/api/user/{userId}/sales")]
        public ActionResult<Sale> GetSalesBySellerId(int? userId)
        {
            var sales = _saleService.GetSaleBySellerId(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [HttpGet("/api/user/{userId}/buy")]
        public ActionResult<Sale> GetSalesByBuyerId(int? userId)
        {
            var sales = _saleService.GetSaleByBuyerId(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [HttpPost("/api/user/{userId}/sales")]
        public ActionResult<int> CreateSaleBySellerId(int userId, SaleBySellerRequest saleRequest)
        {
            try
            {
                Sale sale = saleRequest.ConvertToEntity(userId);
                var id = _saleService.CreateSaleByUserId(sale);
                return CreatedAtAction(nameof(GetSaleById), new { saleId = id }, id);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPatch("{saleId}/product/{productId}/price/{unitPrice}")]
        public ActionResult UpdateUnitPrice(int saleId, int productId, decimal unitPrice)
        {
            try
            {
                _saleService.UpdateUnitPrice(saleId, productId, unitPrice);
                return NoContent();
            }
            catch(ArgumentException ex){
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound();
            }            
        }

        [HttpPatch("{saleId}/product/{productId}/amount/{amount}")]

        public ActionResult UpdateAmount (int saleId, int productId, int amount)
        {

            try
            {

                _saleService.UpdateAmount(saleId, productId, amount);
                return NoContent();

            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName.Equals("saleId") || ex.ParamName.Equals("productId"))
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("/api/user/{userId}/buy")]
        public ActionResult<int> CreateSaleByBuyerId(int userId, SaleByBuyerRequest saleRequest)
        {
            try
            {
                Sale sale = saleRequest.ConvertToEntity(userId);
                var id = _saleService.CreateSaleByUserId(sale);
                return CreatedAtAction(nameof(GetSaleById), new { saleId = id }, id);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.ParamName);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{saleId}/deliver")]
        public ActionResult<int> CreateDeliveryForASale(int saleId, DeliveryRequest deliveryRequest)
        {
            try
            {
                if(deliveryRequest.AddressId <= 0)
                    return BadRequest("AddressId não pode ser nulo nem zero.");

                if (deliveryRequest.DeliveryForecast == DateTime.MinValue)
                    deliveryRequest.DeliveryForecast = DateTime.Now.AddDays(7).ToUniversalTime();

                if (deliveryRequest.DeliveryForecast < DateTime.Now.ToUniversalTime())
                    return BadRequest("Data e horário não podem ser anterior ao atual.");

                Delivery delivery = deliveryRequest.ConvertToEntity(saleId);

                int id = _saleService.CreateDeliveryForASale(delivery);

                return CreatedAtAction(nameof(GetDeliveryById), new { deliveryId = id }, id);
            }
            catch (ArgumentException ex)
            {
                
                if(ex.ParamName.Equals("saleId") || ex.ParamName.Equals("AddressId"))
                    return NotFound(ex.Message);

                
                return BadRequest(ex.Message);

            }

        }

        [HttpGet("/api/delivery/{deliveryId}")]
        public ActionResult<Delivery> GetDeliveryById(int deliveryId)
        {
            Delivery delivery = _saleService.GetDeliveryById(deliveryId);
            if (delivery == null)
                return NoContent();
            return Ok(delivery);
        }

    }
}