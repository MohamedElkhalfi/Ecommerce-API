using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Model;
using Ecommerce.DataAccess.ConnexionDB;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Model;
using Ecommerce.DataAccess.Dto.Interfaces;
using Ecommerce.Core.Exceptions;
using Polly;
using Polly.Retry;
using log4net;
using log4net.Config;
using log4net.Repository;
using System.IO;
using System.Reflection;
using Ecommerce.DataAccess.Transverse;

namespace Ecommerce.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private EcommerceContext _Context { get; }
        private IProductMapping _IProductMapping { get; }
        private readonly AsyncRetryPolicy _retryPolicy;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public ProductRepository(EcommerceContext Context, IProductMapping iproductMapping)
        {
            _Context = Context;
            _IProductMapping = iproductMapping;

            // Configuration de log4net
            ConfigureLogging();

            // Initialisation de la stratégie de retry avec gestion conditionnelle
            _retryPolicy = Policy
                .Handle<Exception>(ex =>
                {
                    if (ex is DbUpdateException dbEx && dbEx.InnerException?.Message.Contains("constraint") == true)
                    {
                        _logger.Warn($"[Politique de Retry] Ignorer la tentative de retry pour violation de contrainte : {dbEx.Message}");
                        return false; // Ne pas réessayer
                    }

                    if (ex is ArgumentException argEx)
                    {
                        _logger.Warn($"[Politique de Retry] Ignorer la tentative de retry pour exception d'argument : {argEx.Message}");
                        return false; // Ne pas réessayer
                    }

                    _logger.Info($"[Politique de Retry] Tentative de retry pour l'exception : {ex.Message}");
                    return true; // Réessayer pour d'autres exceptions
                })
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.Warn($"[Tentative de retry {retryCount}] Attente de {timeSpan.TotalSeconds} secondes avant de réessayer. Exception : {exception.Message}");
                    });
        }

        private void ConfigureLogging()
        {
            var repository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public async Task<int> DeleteProductRepositoryAsync(int ProductID)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    _logger.Info($"[DeleteProductRepositoryAsync] Tentative de suppression du produit avec l'ID : {ProductID}");
                    var product = _Context.Product.SingleOrDefault(op => op.ID.Equals(ProductID));

                    if (product != null)
                    {
                        _Context.Remove(product);
                        int result = await _Context.SaveChangesAsync();
                        _logger.Info($"[DeleteProductRepositoryAsync] Suppression réussie du produit avec l'ID : {ProductID}");
                        return result;
                    }

                    _logger.Warn($"[DeleteProductRepositoryAsync] Aucun produit trouvé avec l'ID : {ProductID}");
                    return 0;
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"[DeleteProductRepositoryAsync] Une erreur est survenue : {ex.Message}", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<ProductModel> FindByIdProductRepositoryAsync(int ProductID)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    _logger.Info($"[FindByIdProductRepositoryAsync] Recherche du produit avec l'ID : {ProductID}");
                    var product = await _Context.Product
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
                        })
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    if (product == null)
                    {
                        _logger.Warn($"[FindByIdProductRepositoryAsync] Aucun produit trouvé avec l'ID : {ProductID}");
                    }

                    return product;
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"[FindByIdProductRepositoryAsync] Une erreur est survenue : {ex.Message}", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> FindByNameProductRepositoryAsync(string Name)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    _logger.Info($"[FindByNameProductRepositoryAsync] Recherche de produits contenant le nom : {Name}");
                    var products = await _Context.Product
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
                        })
                        .AsNoTracking()
                        .ToListAsync();

                    _logger.Info($"[FindByNameProductRepositoryAsync] {products.Count()} produits trouvés avec le nom contenant : {Name}");
                    return products;
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"[FindByNameProductRepositoryAsync] Une erreur est survenue : {ex.Message}", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<int> CreateProductRepositoryAsync(ProductModel _ProductModel)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    _logger.Info($"[CreateProductRepositoryAsync] Création d'un nouveau produit avec le nom : {_ProductModel.Name}");
                    var product = _IProductMapping.CreateProduitProductCoreToDataAccess(_ProductModel);
                    await _Context.Product.AddAsync(product);
                    int result = await _Context.SaveChangesAsync();

                    _logger.Info($"[CreateProductRepositoryAsync] Produit créé avec succès avec l'ID : {product.ID}");
                    return result;
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"[CreateProductRepositoryAsync] Une erreur est survenue : {ex.Message}", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseWriting, ex.InnerException);
            }
        }

        public async Task<int> UpdateProductRepositoryAsync(int _ProductID, ProductModel _ProductModel)
        {
            try
            {
                _logger.Info($"[UpdateProductRepositoryAsync] Mise à jour du produit avec l'ID : {_ProductID}");
                var _ProductMapping = _IProductMapping.CreateProduitProductCoreToDataAccess(_ProductModel);
                var _ProductDb = _Context.Product.Where(x => x.ID.Equals(_ProductID)).Select(x => x).FirstOrDefault();
                var result = 0;
                if (_ProductDb != null)
                {
                    _Context.Product.Update(_ProductMapping);
                    result = await _Context.SaveChangesAsync().ConfigureAwait(false);
                    _logger.Info($"[UpdateProductRepositoryAsync] Produit mis à jour avec succès avec l'ID : {_ProductID}");
                }
                else
                {
                    _logger.Warn($"[UpdateProductRepositoryAsync] Aucun produit trouvé avec l'ID : {_ProductID}");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"[UpdateProductRepositoryAsync] Une erreur est survenue : {ex.Message}", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> ViewAllProductsRepositoryAsync()
        {
            try
            {
                _logger.Info("[ViewAllProductsRepositoryAsync] Récupération de tous les produits");
                IEnumerable<ProductModel> ProductQuery = await _Context.Product
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
                    }).AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

                _logger.Info($"[ViewAllProductsRepositoryAsync] {ProductQuery.Count()} produits récupérés");
                return ProductQuery;
            }
            catch (Exception ex)
            {
                _logger.Error("[ViewAllProductsRepositoryAsync] Une erreur est survenue lors de la récupération des produits", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public Task<ProductModel> FindBySelectedProductRepositoryAsync(bool? IsSelected)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>> UpdateProductSelectedRepositoryAsync(int ProductID, bool? Selected)
        {
            try
            {
                _logger.Info($"[UpdateProductSelectedRepositoryAsync] Mise à jour du statut sélectionné du produit avec l'ID : {ProductID}");
                var getProductByIdAndBySelectedQuery = _Context.Product.SingleOrDefault(op => op.ID.Equals(ProductID));

                if (getProductByIdAndBySelectedQuery != null)
                {
                    getProductByIdAndBySelectedQuery.Is_Selected = Selected;
                    _Context.SaveChanges();
                    _logger.Info($"[UpdateProductSelectedRepositoryAsync] Statut sélectionné mis à jour pour le produit avec l'ID : {ProductID}");
                }

                var ProductQuery = await getProductByID(ProductID);
                return ProductQuery;
            }
            catch (Exception ex)
            {
                _logger.Error("[UpdateProductSelectedRepositoryAsync] Une erreur est survenue lors de la mise à jour", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }

        public async Task<IEnumerable<ProductModel>> getProductByID(int ProductID)
        {
            try
            {
                _logger.Info($"[getProductByID] Recherche de produit avec l'ID : {ProductID}");
                IEnumerable<ProductModel> ProductQuery = await _Context.Product
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
                    }).AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

                _logger.Info($"[getProductByID] {ProductQuery.Count()} produits trouvés avec l'ID : {ProductID}");
                return ProductQuery;
            }
            catch (Exception ex)
            {
                _logger.Error("[getProductByID] Une erreur est survenue lors de la recherche", ex);
                throw new TechnicalException(ExceptionsConstants.MessageErrorDatabaseReading, ex.InnerException);
            }
        }
    }
}
