 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Model;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<int> CreateProductRepositoryAsync(ProductModel Product);
        Task<int> UpdateProductRepositoryAsync(int IDProduct, ProductModel Product);
        Task<int> DeleteProductRepositoryAsync(int IDProduct);
        Task<IEnumerable< ProductModel>> ViewAllProductsRepositoryAsync();

        Task<IEnumerable<ProductModel>> FindByNameProductRepositoryAsync(string Name);
        Task<ProductModel> FindByIdProductRepositoryAsync(int IDProduct);
        Task<ProductModel> FindBySelectedProductRepositoryAsync(bool? IsSelected);
        Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int IDProduct, bool? Selected);


    }
}
