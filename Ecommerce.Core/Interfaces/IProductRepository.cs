 
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
        Task<int> UpdateProductRepositoryAsync(int ProductID, ProductModel Product);
        Task<int> DeleteProductRepositoryAsync(int ProductID);
        Task<IEnumerable< ProductModel>> ViewAllProductsRepositoryAsync();

        Task<IEnumerable<ProductModel>> FindByNameProductRepositoryAsync(string Name);
        Task<ProductModel> FindByIdProductRepositoryAsync(int ProductID);
        Task<ProductModel> FindBySelectedProductRepositoryAsync(bool? IsSelected);
        Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int ProductID, bool? Selected);


    }
}
