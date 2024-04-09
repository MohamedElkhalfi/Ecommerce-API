
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
using Ecommerce.Core.Exceptions;
using Ecommerce.DataAccess.Transverse;

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
            try
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
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<ProductModel> FindByIdProductRepositoryAsync(int ProductID)
        {
            try
            {
                ProductModel ProductQuery = null;
                //await _retryPolicy.ExecuteAsync(async () =>
                //{
                ProductQuery = await (_Context.Product
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
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> FindByNameProductRepositoryAsync(string Name)
        {
            try
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
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public Task<ProductModel> FindBySelectedProductRepositoryAsync(bool? IsSelected)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> ViewAllProductsRepositoryAsync()
        {
            try
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
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<int> CreateProductRepositoryAsync(ProductModel _ProductModel)
        {
            try
            {
                var _ProductMapping = _IProductMapping.CreateProduitProductCoreToDataAccess(_ProductModel);

                var result = 0;
                await _Context.Product.AddAsync(_ProductMapping).ConfigureAwait(false);
                result = await _Context.SaveChangesAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseWriting, ex.InnerException);
            }
        }

        public async Task<int> UpdateProductRepositoryAsync(int _ProductID, ProductModel _ProductModel)
        {
            try
            {
                var _ProductMapping = _IProductMapping.CreateProduitProductCoreToDataAccess(_ProductModel);
                var _ProductDb = _Context.Product.Where(x => x.ID.Equals(_ProductID)).Select(x => x).FirstOrDefault();
                var result = 0;
                if (_ProductDb != null)
                {
                    _Context.Product.Update(_ProductMapping);
                    result = await _Context.SaveChangesAsync().ConfigureAwait(false);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int ProductID, bool? Selected)
        {
            try
            {
                var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(ProductID));

                if (getProductByIdAndBySelectedQuery != null)
                {
                    getProductByIdAndBySelectedQuery.Is_Selected = Selected;
                    _Context.SaveChanges();
                }
                var ProductQuery = await getProductByID(ProductID);

                return ProductQuery;
            }
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> getProductByID(int ProductID)
        {
            try
            {
                IEnumerable<ProductModel> ProductQuery = null;
                //await _retryPolicy.ExecuteAsync(async () =>
                //{
                ProductQuery = await (_Context.Product
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
                                       .ToListAsync()
                                       .ConfigureAwait(false);

                //}).ConfigureAwait(false);
                return ProductQuery;
            }
            catch (Exception ex)
            {
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }


    }
}
