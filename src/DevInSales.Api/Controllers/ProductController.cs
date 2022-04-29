using DevInSales.Core.Entities;
using DevInSales.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("{id}")]
        public ActionResult<Product> ObterProdutoPorId(int id) {
            var produto = _productService.ObterProductPorId(id);
            if(produto == null ) 
                return NotFound();
            return Ok(produto);
        }
    }
}