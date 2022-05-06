using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public void Atualizar(Product produtoOriginal, Product produtoAtualizado)
        {
            produtoOriginal.AtualizarDados(produtoAtualizado);
            _context.SaveChanges();
        }

        // obtém o produto por id 
        public Product? ObterProductPorId(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        // verifica se o nome já existe na base de dados
        public bool ProdutoExiste(string nome)
        {
            var produtos = _context.Products.Where(produto => (produto.Name == nome)).ToList();
            return produtos.Count > 0 ? true : false;
        }
        public void Delete(int id)
        {
            var produto = ObterProductPorId(id);
            if (produto == null)
                throw new Exception("o Produto não existe");
            _context.Products.Remove(produto);
            _context.SaveChanges();
        }

        public List<Product> ObterProdutos(string? name, decimal? priceMin, decimal? priceMax)
        {
            
            if (name != null) 
                return _context.Products.Where(p => p.Name.Contains(name)).ToList();

            if (priceMin != null && priceMax != null)
                return _context.Products.Where(p => p.SuggestedPrice >= priceMin && p.SuggestedPrice <= priceMax).ToList();

            return _context.Products.ToList();
        }

        public int CreateNewProduct(Product product)
        {
            var ProductValidate = _context.Products.Any(p => p.Name == product.Name);
            if (ProductValidate)
                return -1;
            if (product.SuggestedPrice <= 0)
                return -1;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }
    }
}