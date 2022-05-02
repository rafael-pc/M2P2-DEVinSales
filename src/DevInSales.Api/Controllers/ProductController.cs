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
        
        [HttpPut("{id}")]
        public ActionResult AtualizarProduto(Product product,int id) {
            var produto  = _productService.ObterProductPorId(id);
            
            if(produto == null ) 
                return NotFound();
            if(!ModelState.IsValid || product.Name.ToLower() == "string") 
                return BadRequest("O objeto tem que ser construido com um nome e nome tem que ser diferente de string");
            if(_productService.ProdutoExiste(product.Name)) 
                return BadRequest("esse nome já existe na base de dados");

            _productService.Atualizar(produto,product);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            try{
                _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex){
                if(ex.Message.Contains("não existe"))
                    return NotFound();
                if(ex.HResult == -2146233088 )  
                    return BadRequest("O produto especificado não pode ser excluido, pq já está atrelado a outra tabela!");

                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });    
            }
            
        }
    }
}