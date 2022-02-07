using Ecommerce.Api.Dto.Interfaces.BusinessToApi;
using Ecommerce.Api.Model;
using Ecommerce.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Dto.Mapping
{
    public class ProductApiMap : IProductInterface
    {


        public IEnumerable<ProductApi>  ViewAllProductsMap(IEnumerable<ProductModel> ProductService)
        {
            IList<ProductApi> ProductApiMap = new List<ProductApi>();
            foreach (var pi in ProductService)
            {
                var _ProductApi = new ProductApi();
                _ProductApi.id = pi. ID;
                _ProductApi.name = pi.Name ;
                _ProductApi.description  =pi.Description ;
                _ProductApi.available  =pi.Is_Available ;
                _ProductApi.selected  =pi.Is_Selected ;
                _ProductApi.photoName  =pi.PhotoName ;
                _ProductApi.quantity  =pi.Quantity ;
                _ProductApi.currentPrice  =pi.CurrentPrice ;
                ProductApiMap.Add(_ProductApi);
            }

            return ProductApiMap;
        }

       
    }
}
