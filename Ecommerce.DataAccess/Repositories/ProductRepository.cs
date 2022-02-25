
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Model;
using Ecommerce.DataAccess.ConnexionDB;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Model;
using Ecommerce.DataAccess.Dto.Interfaces;

namespace Ecommerce.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private EcommerceContext _Context { get; }
        private IProductMapping _IProductMapping { get; }
        private readonly Polly.Retry.AsyncRetryPolicy _retryPolicy;

        public ProductRepository(EcommerceContext Context, IProductMapping iproductMapping)
        {
            _Context = Context;
            _IProductMapping = iproductMapping;
        }

        public async Task<int> DeleteProductRepositoryAsync(int ProductID)
        {
            var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(ProductID));

            var result = 0;

            if (getProductByIdAndBySelectedQuery != null)
            {
                _Context.Remove(getProductByIdAndBySelectedQuery);
                result = await _Context.SaveChangesAsync();
            }
           

            return result;
        }

        public async Task<ProductModel> FindByIdProductRepositoryAsync(int ProductID)
        {
             ProductModel ProductQuery = null;
            //await _retryPolicy.ExecuteAsync(async () =>
            //{
            ProductQuery = await(_Context.Product
                                 .Where(lm => lm.ID.Equals(ProductID))
                                 .Select(op => new ProductModel
                                 {
                                     ID = op.ID,
                                     CurrentPrice = op.CurrentPrice,
                                     Description = op.Description,
                                     Is_Available = op.Is_Available,
                                     Is_Promotion = op.Is_Promotion,
                                     Is_Selected = op.Is_Selected,
                                     Name = op.Name,
                                     PhotoName = op.PhotoName,
                                     Quantity = op.Quantity
                                 })).AsNoTracking()
                                   .FirstOrDefaultAsync()
                                   .ConfigureAwait(false);

            //}).ConfigureAwait(false);
            return ProductQuery;
        }

        public async Task<IEnumerable<ProductModel>> FindByNameProductRepositoryAsync(string Name)
        {
            IEnumerable<ProductModel> ProductQuery = null;
            //await _retryPolicy.ExecuteAsync(async () =>
            //{
            ProductQuery = await (_Context.Product
                                 .Where(lm => lm.Name.Contains(Name))
                                 .Select(op => new ProductModel
                                 {
                                     ID = op.ID,
                                     CurrentPrice = op.CurrentPrice,
                                     Description = op.Description,
                                     Is_Available = op.Is_Available,
                                     Is_Promotion = op.Is_Promotion,
                                     Is_Selected = op.Is_Selected,
                                     Name = op.Name,
                                     PhotoName = op.PhotoName,
                                     Quantity = op.Quantity
                                 })).AsNoTracking()
                                   .ToListAsync()
                                   .ConfigureAwait(false);

            //}).ConfigureAwait(false);
            return ProductQuery;
        }

        public Task<ProductModel> FindBySelectedProductRepositoryAsync(bool? IsSelected)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> ViewAllProductsRepositoryAsync()
        {
            IEnumerable<ProductModel> ProductQuery = null;
             //await _retryPolicy.ExecuteAsync(async () =>
                        //{
                            ProductQuery = await (_Context.Product
                                                  .Select(op => new ProductModel
                                                  {
                                                      ID = op.ID,
                                                      CurrentPrice = op.CurrentPrice,
                                                      Description = op.Description,
                                                      Is_Available = op.Is_Available,
                                                      Is_Promotion = op.Is_Promotion,
                                                      Is_Selected = op.Is_Selected,
                                                      Name = op.Name,
                                                      PhotoName = op.PhotoName,
                                                      Quantity = op.Quantity
                                                  })).AsNoTracking()
                                                    .ToListAsync()
                                                    .ConfigureAwait(false);

                        //}).ConfigureAwait(false);
            return ProductQuery;
        }

        public async Task<int> CreateProductRepositoryAsync(ProductModel _ProductModel)
        {
            var _ProductMapping = _IProductMapping.CreateProduitProductCoreToDataAccess(_ProductModel);

            var result = 0;
            await _Context.Product.AddAsync(_ProductMapping).ConfigureAwait(false);
            result = await _Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        public Task<int> UpdateProductRepositoryAsync(int ProductID, ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int ProductID, bool? Selected)
        {
            var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(ProductID)); 
                           
            if(getProductByIdAndBySelectedQuery != null  )
            {
                getProductByIdAndBySelectedQuery.Is_Selected = Selected;
                _Context.SaveChanges();
            }
            var  ProductQuery =  await getProductByID(  ProductID);
           
            return ProductQuery;
        }

        public async Task<IEnumerable<ProductModel>> getProductByID(int ProductID)
        {
            IEnumerable < ProductModel> ProductQuery = null;
            //await _retryPolicy.ExecuteAsync(async () =>
            //{
            ProductQuery = await (_Context.Product
                                 .Where(lm => lm.ID.Equals(ProductID) )
                                 .Select(op => new ProductModel
                                 {
                                     ID = op.ID,
                                     CurrentPrice = op.CurrentPrice,
                                     Description = op.Description,
                                     Is_Available = op.Is_Available,
                                     Is_Promotion = op.Is_Promotion,
                                     Is_Selected = op.Is_Selected,
                                     Name = op.Name,
                                     PhotoName = op.PhotoName,
                                     Quantity = op.Quantity
                                 })).AsNoTracking()
                                   .ToListAsync()
                                   .ConfigureAwait(false);

            //}).ConfigureAwait(false);
            return ProductQuery;
        }

     
    }
}
