using  Ecommerce.Core.Model; 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Ecommerce.Core.Interfaces
{
    public interface IProductService
    { 
        Task<IEnumerable< ProductModel>> ViewAllProductsServiceAsync();
        Task<IEnumerable<ProductModel>> UpdateProductSelectedServiceAsync(int IDProduct, bool? Selected);
        Task<int> DeleteProductServiceAsync(int IDProduct);
        Task<IEnumerable<ProductModel>> FindByNameProductServiceAsync(string Name);
        Task<int> CreateProductServiceAsync(ProductModel _ProductModel);
        Task<ProductModel> FindByIdProductServiceAsync(int ProductID);
        Task<int> UpdateProductServiceAsync(int ProductID, ProductModel _ProductModel);

    }
}
