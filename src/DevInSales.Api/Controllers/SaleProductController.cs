using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("api/sales/")]

    public class SaleProductController : ControllerBase
    {
        private readonly ISaleProductService _saleProductService;

        public SaleProductController(ISaleProductService saleProductService)
        {
            _saleProductService = saleProductService;
        }

        [HttpGet("saleById/item")]

        public ActionResult<int> GetSaleProductById(int saleProductId)
        {
            var id = _saleProductService.GetSaleProductById(saleProductId);
            if (id == null)
                return NotFound();

            return Ok(id);
        }

        [HttpPost("{saleId}/item")]

        public ActionResult<int> CreateSaleProduct(int saleId, SaleProductRequest saleProduct)
        {
            try
            {
                if (saleProduct.ProductId <= 0)
                    return BadRequest();

                if (saleProduct.Amount == null)
                    saleProduct.Amount = 1;
                

                var id = _saleProductService.CreateSaleProduct(saleId, saleProduct);
                return CreatedAtAction(nameof(GetSaleProductById), new { saleProductId = id }, id);

                
            }
            catch(ArgumentException ex)
            {
                if(ex.Message.Contains("não encontrado."))
                    return NotFound();

                if(ex.Message.Contains("não podem ser negativos."))
                    return BadRequest();

                return BadRequest();
            
            }

      
            


        }



    }
}
