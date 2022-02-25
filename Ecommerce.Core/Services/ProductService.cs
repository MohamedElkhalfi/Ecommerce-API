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
         
        public async Task<IEnumerable<ProductModel>> ViewAllProductsServiceAsync()
        {
            return await _productRepository.ViewAllProductsRepositoryAsync();
        }
         
        public async Task<IEnumerable<ProductModel>> UpdateProductSelectedServiceAsync(int IDProduct, bool? Selected)
        {
            return await _productRepository.UpdateProductSelectedRepositoryAsync(IDProduct, Selected);

        }

        public async Task<int> DeleteProductServiceAsync(int IDProduct)
        {
            return await _productRepository.DeleteProductRepositoryAsync(IDProduct);
        }

        public async Task<IEnumerable<ProductModel>> FindByNameProductServiceAsync(string Name)
        {
            return await _productRepository.FindByNameProductRepositoryAsync(Name);
        }

        public async Task<int> CreateProductServiceAsync(ProductModel _ProductModel)
        {
            return await _productRepository.CreateProductRepositoryAsync(_ProductModel);
               
        }

        public async Task<ProductModel> FindByIdProductServiceAsync(int ProductID)
        {
            return await _productRepository.FindByIdProductRepositoryAsync(ProductID);
        }
    }
}
