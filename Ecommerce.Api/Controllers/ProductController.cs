using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Transverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Core.Model;
using AutoMapper;
using Ecommerce.Api.Model;
using Ecommerce.Api.Dto.Interfaces.BusinessToApi;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Ecommerce.Api.Errors;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using log4net;
using log4net.Config;
using log4net.Repository;
using System.Reflection;
using System.IO;

namespace Ecommerce.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    //[EnableCors("MohamedOrganization")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _ProductService;
        private readonly IProductInterface _ProductInterface;
        private readonly ILogger<ProductController> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        // Injection des dépendances et configuration du logger et de Polly
        public ProductController(IProductService productService, IMapper mapper = null, IProductInterface productInterface = null, ILogger<ProductController> logger = null)
        {
            _ProductService = productService;
            _mapper = mapper;
            _ProductInterface = productInterface;
            _logger = logger;

            // Configuration de Polly pour les retries
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger?.LogWarning($"[Retry Attempt {retryCount}] Waiting {timeSpan.TotalSeconds} seconds before retry. Exception: {exception.Message}");
                    });
        }

        [HttpGet("ViewAllProducts")]
        //[Authorize(Policy = "MohamedOrganization")]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ViewAllProducts()
        {
            try
            {
                _logger?.LogInformation("Début de la récupération de tous les produits.");

                var productResponse = await _retryPolicy.ExecuteAsync(() => _ProductService.ViewAllProductsServiceAsync());
                if (productResponse == null || !productResponse.Any())
                {
                    _logger?.LogWarning("Aucun produit trouvé.");
                    return NoContent();
                }

                var ProductMapped = _ProductInterface.ViewAllProductsMap(productResponse);
                return Ok(ProductMapped);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la récupération des produits.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }

        [HttpGet("UpdateProductSelected")]
        //[Authorize(Policy = "AllowAll")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProductSelected([Required, FromQuery] int idProduct, [Required, FromQuery] bool? selected)
        {
            try
            {
                _logger?.LogInformation($"Mise à jour de la sélection du produit avec l'ID {idProduct}.");
                var Response = await _retryPolicy.ExecuteAsync(() => _ProductService.UpdateProductSelectedServiceAsync(idProduct, selected));

                if (Response == null)
                {
                    _logger?.LogWarning($"Produit avec ID {idProduct} non trouvé.");
                    return NoContent();
                }

                var ProductMapped = _ProductInterface.UpdateProductSelectedModelToApiProductMap(Response);
                return Ok(ProductMapped);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la mise à jour du produit sélectionné.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }

        [HttpDelete("DeleteProductAsync")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductAsync([Required, FromQuery] int idProduct)
        {
            try
            {
                _logger?.LogInformation($"Tentative de suppression du produit avec l'ID {idProduct}.");
                var result = await _retryPolicy.ExecuteAsync(() => _ProductService.DeleteProductServiceAsync(idProduct));
                if (result <= 0)
                {
                    _logger?.LogWarning($"Produit avec l'ID {idProduct} non trouvé.");
                    return NotFound(idProduct);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la suppression du produit.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }

        [HttpPost("CreateProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProducts([FromBody] ProductApi productApi)
        {
            try
            {
                _logger?.LogInformation("Tentative de création d'un nouveau produit.");
                var ProductModelMapped = _ProductInterface.CreateProduitProductApiToModelProductMap(productApi);
                var result = await _retryPolicy.ExecuteAsync(() => _ProductService.CreateProductServiceAsync(ProductModelMapped));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la création du produit.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }

        [HttpGet("FindProductsByIdAsync")]
        //[Authorize(Policy = "AllowAll")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProductApi), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FindProductsByIdAsync([Required, FromQuery] int ProductID)
        {
            try
            {
                _logger?.LogInformation($"Recherche du produit avec l'ID {ProductID}.");
                var Response = await _retryPolicy.ExecuteAsync(() => _ProductService.FindByIdProductServiceAsync(ProductID));

                if (Response == null)
                {
                    _logger?.LogWarning($"Produit avec l'ID {ProductID} non trouvé.");
                    return NoContent();
                }

                var result = _ProductInterface.FindByIdProductModelToApiProductMap(Response);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la recherche du produit par ID.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }

        [HttpPut("UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateProductAsync([Required, FromQuery] int _ProductID, [FromBody] ProductApi _ProductApi)
        {
            try
            {
                if (_ProductID < 0 || _ProductApi == null)
                {
                    return BadRequest();
                }

                _logger?.LogInformation($"Mise à jour du produit avec l'ID {_ProductID}.");
                var ProductModelMapped = _ProductInterface.UpdateProduitProductApiToModelProductMap(_ProductApi);
                var result = await _retryPolicy.ExecuteAsync(() => _ProductService.UpdateProductServiceAsync(_ProductID, ProductModelMapped));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Erreur lors de la mise à jour du produit.");
                return StatusCode(500, new { message = "Erreur interne du serveur" });
            }
        }
    }
}
