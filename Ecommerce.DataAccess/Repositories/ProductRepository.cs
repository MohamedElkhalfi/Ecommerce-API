﻿
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

namespace Ecommerce.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private EcommerceContext _Context { get; }
        private readonly Polly.Retry.AsyncRetryPolicy _retryPolicy;

        public ProductRepository(EcommerceContext Context)
        {
            _Context = Context;
        }

        public async Task<int> DeleteProductRepositoryAsync(int IDProduct)
        {
            var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(IDProduct));

            var result = 0;

            if (getProductByIdAndBySelectedQuery != null)
            {
                _Context.Remove(getProductByIdAndBySelectedQuery);
                result = await _Context.SaveChangesAsync();
            }
           

            return result;
        }

        public Task<ProductModel> FindByIdProductRepositoryAsync(int IDProduct)
        {
            throw new NotImplementedException();
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

        public Task<int> CreateProductRepositoryAsync(ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProductRepositoryAsync(int IDProduct, ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int IDProduct, bool? Selected)
        {
            var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(IDProduct)); 
                           
            if(getProductByIdAndBySelectedQuery != null  )
            {
                getProductByIdAndBySelectedQuery.Is_Selected = Selected;
                _Context.SaveChanges();
            }
            var  ProductQuery =  await getProductByID(  IDProduct);
           
            return ProductQuery;
        }

        public async Task<IEnumerable<ProductModel>> getProductByID(int IDProduct)
        {
            IEnumerable < ProductModel> ProductQuery = null;
            //await _retryPolicy.ExecuteAsync(async () =>
            //{
            ProductQuery = await (_Context.Product
                                 .Where(lm => lm.ID.Equals(IDProduct) )
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