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

        public ProductModel ApiToModelProductMap(ProductApi _ProductApi)
        {  
                var _ProductModel = new ProductModel();
            _ProductModel.ID = _ProductApi.id ;
            _ProductModel.Name  =_ProductApi.name;
            _ProductModel.Description =  _ProductApi.description;
            _ProductModel.Is_Available =  _ProductApi.available;
            _ProductModel.Is_Selected = _ProductApi.selected;
            _ProductModel.PhotoName =  _ProductApi.photoName;
            _ProductModel.Quantity = _ProductApi.quantity;
            _ProductModel.CurrentPrice =  _ProductApi.currentPrice;
                 
            return _ProductModel;
        }

        public ProductApi ModelToApiProductMap(ProductModel _ProductModel)
        {
            var _ProductApi = new ProductApi();
            _ProductApi.id = _ProductModel.ID;
            _ProductApi.name = _ProductModel.Name;
            _ProductApi.description = _ProductModel.Description;
            _ProductApi.available = _ProductModel.Is_Available;
            _ProductApi.selected = _ProductModel.Is_Selected;
            _ProductApi.photoName = _ProductModel.PhotoName;
            _ProductApi.quantity = _ProductModel.Quantity;
            _ProductApi.currentPrice = _ProductModel.CurrentPrice;

            return _ProductApi;
        }

        public IEnumerable<ProductApi> UpdateProductSelectedModelToApiProductMap(IEnumerable<ProductModel> ProductService)
        {
            IList<ProductApi> ProductApiMap = new List<ProductApi>();
            foreach (var pi in ProductService)
            {
                var _ProductApi = new ProductApi();
                _ProductApi.id = pi.ID;
                _ProductApi.name = pi.Name;
                _ProductApi.description = pi.Description;
                _ProductApi.available = pi.Is_Available;
                _ProductApi.selected = pi.Is_Selected;
                _ProductApi.photoName = pi.PhotoName;
                _ProductApi.quantity = pi.Quantity;
                _ProductApi.currentPrice = pi.CurrentPrice;
                ProductApiMap.Add(_ProductApi);
            }

            return ProductApiMap;
        }

        public IEnumerable<ProductApi> FindByNameProductModelToApiProductMap(IEnumerable<ProductModel> ProductService)
        {
            IList<ProductApi> ProductApiMap = new List<ProductApi>();
            foreach (var pi in ProductService)
            {
                var _ProductApi = new ProductApi();
                _ProductApi.id = pi.ID;
                _ProductApi.name = pi.Name;
                _ProductApi.description = pi.Description;
                _ProductApi.available = pi.Is_Available;
                _ProductApi.selected = pi.Is_Selected;
                _ProductApi.photoName = pi.PhotoName;
                _ProductApi.quantity = pi.Quantity;
                _ProductApi.currentPrice = pi.CurrentPrice;
                ProductApiMap.Add(_ProductApi);
            }

            return ProductApiMap;
        }

        public ProductModel CreateProduitProductApiToModelProductMap(ProductApi _ProductApi)
        {
            var _ProductModel = new ProductModel();
            _ProductModel.ID = _ProductApi.id;
            _ProductModel.Name = _ProductApi.name;
            _ProductModel.Description = _ProductApi.description != null ? _ProductApi.description  : "";
            _ProductModel.Is_Available = _ProductApi.available;
            _ProductModel.Is_Selected = _ProductApi.selected;
            _ProductModel.PhotoName = _ProductApi.photoName != null ? _ProductApi.photoName : "";
            _ProductModel.Is_Promotion = _ProductApi.promotion != null ? _ProductApi.promotion : true;
            _ProductModel.Quantity = _ProductApi.quantity;
            _ProductModel.CurrentPrice = _ProductApi.currentPrice;

            return _ProductModel;
        }

        public  ProductApi  FindByIdProductModelToApiProductMap( ProductModel  _ProductService)
        {
            var ProductApiMap = new  ProductApi();
          
                var _ProductApi = new ProductApi();
                _ProductApi.id = _ProductService.ID;
                _ProductApi.name = _ProductService.Name;
                _ProductApi.description = _ProductService.Description;
                _ProductApi.available = _ProductService.Is_Available;
                _ProductApi.selected = _ProductService.Is_Selected;
                _ProductApi.photoName = _ProductService.PhotoName;
                _ProductApi.quantity = _ProductService.Quantity;
                _ProductApi.currentPrice = _ProductService.CurrentPrice;
              

            return _ProductApi;
        }


        public ProductModel UpdateProduitProductApiToModelProductMap(ProductApi _ProductApi)
        {
            var _ProductModel = new ProductModel();
            _ProductModel.ID = _ProductApi.id;
            _ProductModel.Name = _ProductApi.name;
            _ProductModel.Description = _ProductApi.description != null ? _ProductApi.description : "";
            _ProductModel.Is_Available = _ProductApi.available;
            _ProductModel.Is_Selected = _ProductApi.selected;
            _ProductModel.PhotoName = _ProductApi.photoName != null ? _ProductApi.photoName : "";
            _ProductModel.Is_Promotion = _ProductApi.promotion != null ? _ProductApi.promotion : true;
            _ProductModel.Quantity = _ProductApi.quantity;
            _ProductModel.CurrentPrice = _ProductApi.currentPrice;

            return _ProductModel;
        }
    }
}
