
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
        public ActionResult<Sale> GetSales(int? userId)
        {
            var sales = _saleService.GetSellerById(userId);
            if (sales.Count == 0)
                return NoContent();
            return Ok(sales);
        }

        [HttpPost("/user/{userId}/sales")]
        public ActionResult<int> CreateSale(int userId, SaleBySellerRequest saleRequest)
        {
            try
            {
                Sale sale = saleRequest.ConverterParaEntidade(userId);
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