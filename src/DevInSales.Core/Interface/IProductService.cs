using DevInSales.Core.Entities;

namespace DevInSales.Core.Interface
{
    public interface IProductService
    {
        public void Atualizar(Product produtoOriginal, Product produtoAtualizado);
        public Product? ObterProductPorId(int id);
        public bool ProdutoExiste(string nome);
        public void Delete(int id) ;
    }

}