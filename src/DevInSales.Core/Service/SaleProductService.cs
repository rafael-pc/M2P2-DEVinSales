using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Service
{
    public class SaleProductService : ISaleProductService
    {

        private readonly DataContext _context;

        public SaleProductService (DataContext context)
        {
            _context = context;
        }

        
        public int CreateSaleProduct(int saleId, SaleProductRequest saleProduct)
        {

            if (saleProduct.UnitPrice == null)
                saleProduct.UnitPrice = _context.Products.FirstOrDefault(p => p.Id == saleProduct.ProductId).SuggestedPrice;

            if (saleProduct.Amount == null)
                saleProduct.Amount = 1;

            if (saleProduct.ProductId == null)
                throw new ArgumentNullException();

            if (!_context.Products.Any(p => p.Id == saleProduct.ProductId) || !_context.Sales.Any(p => p.Id == saleId))
                throw new ArgumentException("ProductId ou SaleId não encontrado.");

            if (saleProduct.UnitPrice <= 0 || saleProduct.Amount <= 0)
                throw new ArgumentException("Preço ou quantidade não podem ser negativos.");

            
           


            var saleProductEntity = saleProduct.ConvertIntoSaleProduct(saleId);
            _context.SaleProducts.Add(saleProductEntity);
            _context.SaveChanges();

            return saleProductEntity.Id;
        }

        // Criar SaleProductResponse
        public int GetSaleProductById(int saleProductId)
        {
            SaleProduct? saleProduct = _context.SaleProducts
                .Include(p => p.Sales)
                .Include(p => p.Products)
                .FirstOrDefault(p => p.Id == saleProductId);

            if (saleProduct == null)
            {
                // Corrigir retorno
                return 0;
            }

            //Corrigir retorno com SaleProductResponse
            return saleProduct.Id;

        }

        


    }
}
