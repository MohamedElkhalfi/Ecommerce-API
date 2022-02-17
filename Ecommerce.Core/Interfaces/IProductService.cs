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
      
        Task<IEnumerable< ProductModel>> ViewAllProductsAsync();
        Task<ProductModel> UpdateProductSelected(int IDProduct, bool? Selected);
        Task<int> DeleteProductAsync(int IDProduct);

    }
}
