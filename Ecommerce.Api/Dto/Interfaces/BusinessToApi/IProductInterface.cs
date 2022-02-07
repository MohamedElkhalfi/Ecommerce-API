using Ecommerce.Api.Dto.Mapping;
using Ecommerce.Api.Model;
using Ecommerce.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Dto.Interfaces.BusinessToApi
{
   public interface IProductInterface
    {
        IEnumerable<ProductApi> ViewAllProductsMap( IEnumerable<ProductModel>  ProductService);
         
    }
}
