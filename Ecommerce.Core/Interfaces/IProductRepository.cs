 
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
        Task<int> CreateProductAsync(ProductModel Product);
        Task<int> UpdateProductAsync(int IDProduct, ProductModel Product);
        Task<int> DeleteProductAsync(int IDProduct);
        Task<IEnumerable< ProductModel>> ViewAllProductsAsync();

        Task<IEnumerable<ProductModel>> FindByNameProductAsync(string Name);
        Task<ProductModel> FindByIdProductAsync(int IDProduct);
        Task<ProductModel> FindBySelectedProductAsync(bool? IsSelected);

    }
}
