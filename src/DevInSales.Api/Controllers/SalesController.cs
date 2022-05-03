
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("/sales/")]

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

        [HttpGet("/user/{userId}/sales")]
        public ActionResult<Sale> GetSalesBySellerId(int? userId)
        {
            var sales = _saleService.GetSaleBySellerId(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [HttpGet("/user/{userId}/buy")]
        public ActionResult<Sale> GetSalesByBuyerId(int? userId)
        {
            var sales = _saleService.GetSaleByBuyerId(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [HttpPost("/user/{userId}/sales")]
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

        [HttpPost("/user/{userId}/buy")]
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
    }
}