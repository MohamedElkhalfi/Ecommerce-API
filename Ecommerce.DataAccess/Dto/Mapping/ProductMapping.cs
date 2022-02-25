using Ecommerce.Core.Model;
using Ecommerce.DataAccess.Dto.Interfaces;
using Ecommerce.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Dto.Mapping
{
    public class ProductMapping : IProductMapping
    {
        public Product CreateProduitProductCoreToDataAccess(ProductModel _ProductModel)
        {
            var _Product = new Product();
            _Product.ID = _ProductModel.ID;
            _Product.Name = _ProductModel.Name;
            _Product.Description = _ProductModel.Description;
            _Product.Is_Available = _ProductModel.Is_Available;
            _Product.Is_Selected = _ProductModel.Is_Selected;
            _Product.PhotoName = _ProductModel.PhotoName;
            _Product.Quantity = _ProductModel.Quantity;
            _Product.CurrentPrice = _ProductModel.CurrentPrice;

            return _Product;
        }
    }
}
