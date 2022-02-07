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
 

    }
}
