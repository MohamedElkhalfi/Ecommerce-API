using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<IEnumerable<ProductModel>> ViewAllProductsAsync()
        {
            return await _productRepository.ViewAllProductsAsync();
        }



        public async Task<ProductModel> UpdateProductSelected(int IDProduct, bool? Selected)
        {
            return await _productRepository.UpdateProductSelected(IDProduct, Selected);

        }

        public async Task<int> DeleteProductAsync(int IDProduct)
        {
            return await _productRepository.DeleteProductAsync(IDProduct);
        }
    }
}
