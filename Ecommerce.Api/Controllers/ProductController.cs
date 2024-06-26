﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ProductController(IProductService productService, IMapper mapper = null, IProductInterface productInterface = null)
        {
            _ProductService = productService;
            _mapper = mapper;
            _ProductInterface = productInterface;
        }


        [HttpGet("ViewAllProducts")]
        //[Authorize(Policy = "MohamedOrganization")]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ViewAllProducts()
        {


            var productResponse = await _ProductService.ViewAllProductsServiceAsync(); ;
            if (productResponse == null || !productResponse.Any())
            {
                return NoContent();
            }
            var ProductMapped = _ProductInterface.ViewAllProductsMap(productResponse);
            return Ok(ProductMapped);

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


            var Response = await _ProductService.UpdateProductSelectedServiceAsync(idProduct, selected);
            if (Response == null)
            {
                return NoContent();
            }
            var ProductMapped = _ProductInterface.UpdateProductSelectedModelToApiProductMap(Response);

            return Ok(ProductMapped);

        }

        [HttpDelete("DeleteProductAsync")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductAsync([Required, FromQuery] int idProduct)
        {
            var result = await _ProductService.DeleteProductServiceAsync(idProduct);
            if (result <= 0)
            {
                return NotFound(idProduct);
            }

            return Ok(result);
        }


        [HttpGet("FindProductsByName")]
        //[Authorize(Policy = "AllowAll")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), (int)HttpStatusCode.OK)]

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FindProductsByName([Required, FromQuery] string KeyWord)
        {
            var Response = await _ProductService.FindByNameProductServiceAsync(KeyWord);
            if (Response == null)
            {
                return NoContent();
            }
            var ProductMapped = _ProductInterface.FindByNameProductModelToApiProductMap(Response);

            return Ok(ProductMapped);

        }

        [HttpPost("CreateProducts")]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProducts([FromBody] ProductApi productApi)
        {

            var ProductModelMapped = _ProductInterface.CreateProduitProductApiToModelProductMap(productApi);

            var result = await _ProductService.CreateProductServiceAsync(ProductModelMapped);
            return Ok(result);

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
            var Response = await _ProductService.FindByIdProductServiceAsync(ProductID);
            if (Response == null)
            {
                return NoContent();
            }
            var result = _ProductInterface.FindByIdProductModelToApiProductMap(Response);

            return Ok(result);

        }

        [HttpPut("UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateProductAsync([Required, FromQuery] int _ProductID, [FromBody] ProductApi _ProductApi)
        {
            if (_ProductID<0 || _ProductApi == null)
            {
                return BadRequest();
            }

            var ProductModelMapped = _ProductInterface.UpdateProduitProductApiToModelProductMap(_ProductApi);
            var result = await _ProductService.UpdateProductServiceAsync(_ProductID, ProductModelMapped);

          return  Ok(result);
        }



    }
}
