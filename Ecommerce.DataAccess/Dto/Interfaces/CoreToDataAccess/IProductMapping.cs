using Ecommerce.Core.Model;
using Ecommerce.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Dto.Interfaces
{
  public  interface IProductMapping
    {
        Product CreateProduitProductCoreToDataAccess(ProductModel _ProductModel);

    }
}
