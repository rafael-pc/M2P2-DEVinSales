
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interface;
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
    }
}