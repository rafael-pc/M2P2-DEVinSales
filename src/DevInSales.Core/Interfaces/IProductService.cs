using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface IProductService
    {
        public void Atualizar(Product produtoOriginal, Product produtoAtualizado);
        public Product? ObterProductPorId(int id);
        public bool ProdutoExiste(string nome);
        public void Delete(int id);
        public List<Product> ObterProdutos(string? name, decimal? priceMin, decimal? priceMax);
    }

}