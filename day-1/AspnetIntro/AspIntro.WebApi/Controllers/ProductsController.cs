using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Dtos;
using AspIntro.WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            ILogger<ProductsController> logger, 
            IProductsRepository productsRepository, 
            IMapper mapper,
            ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _logger = logger;
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(200, Type = typeof(ProductDto[]))]
        public async Task<IActionResult> GetAll()
        {
            Product[] products = await _productsRepository.GetAll();
            foreach (var item in products)
            {
                item.Price = _currencyService.Change(item.Price, "USD", "NIS");
            }
            return Ok(products.Select(x => _mapper.Map<ProductDto>(x)).ToArray());
        }


        [HttpPut("{id}", Name = "UpdateProduct")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
        {
            try
            {
                dto.Id = id;  // RECORD!
                var productAfterUpdate = await _productsRepository.Update(_mapper.Map<Product>(dto));
                return Ok(productAfterUpdate);
            }
            catch (CourseApiException  ex) when (ex.ErrorCode == ApiExceptionCodes.NotFound)
            {
                return NotFound($"Product ({id}) cannot be found..");
            }
            catch (CourseApiException ex) when (ex.ErrorCode == ApiExceptionCodes.Conflict)
            {
                return Conflict();
            }
        }















    }
}
