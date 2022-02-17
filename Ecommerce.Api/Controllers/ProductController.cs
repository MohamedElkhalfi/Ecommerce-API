using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Api.Mapper.Interface;
 
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Transverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EX = Ecommerce.Core.Exceptions.Constants.MessagesConstantes;
using Ecommerce.Core.Model;
using AutoMapper;
using Ecommerce.Api.Model;
using Ecommerce.Api.Dto.Interfaces.BusinessToApi;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : Controller
    {

      
        private readonly IMapper _mapper;
        private readonly IProductService _ProductService;
        private readonly IProductInterface _ProductInterface;
        public ProductController(IProductService productService, IMapper mapper = null, IProductInterface productInterface = null)
        {
            _ProductService = productService;
            _mapper = mapper;
            _ProductInterface = productInterface;
        }

        /// <summary>
        /// Cette méthode permet configurer une nouvelle offre commerciale
        /// </summary>
        /// <param name="agreementProduct"></param>
        /// <returns></returns>
        [HttpGet("ViewAllProducts")]
        //[Authorize(Policy = "AllowAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductApi>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ViewAllProducts()
        {
           

            var productResponse = await _ProductService.ViewAllProductsAsync(); ;
            if (productResponse == null || !productResponse.Any())
            {
                return NoContent();
            } 
           var ProductMapped=  _ProductInterface.ViewAllProductsMap(productResponse);  
            return Ok(ProductMapped);

        }

        /// <summary>
        /// Cette méthode permet configurer une nouvelle offre commerciale
        /// </summary>
        /// <param name="agreementProduct"></param>
        /// <returns></returns>
        [HttpGet("UpdateProductSelected")]
        //[Authorize(Policy = "AllowAll")]
        [ProducesResponseType(typeof(ProductApi), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProductSelected([Required, FromQuery] int idProduct, [Required, FromQuery] bool? selected)
        {

         
             var Response = await _ProductService.UpdateProductSelected(idProduct, selected); 
            if (Response == null )
            {
                return NoContent();
            }
            var ProductMapped = _ProductInterface.ModelToApiProductMap(Response); 
            return Ok(ProductMapped);

        }

        [HttpDelete("DeleteProductAsync")] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductAsync([Required, FromQuery] int idProduct )
        {
            var result = await _ProductService.DeleteProductAsync(idProduct);
            if (result <= 0)
            {
                return NotFound(idProduct);
            }

            return NoContent();
        }

        }
}
