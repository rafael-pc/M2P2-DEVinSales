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

        [HttpGet("/user/{userId}/buy")]
        public ActionResult<List<Sale>> GetSaleByUserId(int userId)
        {
            var sales = _saleService.GetSaleByUserId(userId);
            if (!sales.Any())
                return NoContent();

            return Ok(sales);
        }

    }
}