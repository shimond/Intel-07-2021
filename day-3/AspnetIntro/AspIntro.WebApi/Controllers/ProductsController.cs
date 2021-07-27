using AspIntro.WebApi.ActionFilters;
using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Dtos;
using AspIntro.WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    //[ServiceFilter(typeof(ValidationActionFilter))]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly MySingleService _mySingleService;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductsRepository productsRepository,
            IMapper mapper,
            MySingleService mySingleService,
            ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _logger = logger;
            _productsRepository = productsRepository;
            _mapper = mapper;
            _mySingleService = mySingleService;
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(CourseApiExceptionActionFilter))]

        public async Task<IActionResult> GetById(int id, string name)
        {
            var product = await _productsRepository.GetById(id);
            return Ok(product);
        }

        [ServiceFilter(typeof(CourseApiExceptionActionFilter))]
        [HttpGet(Name = "GetAllProducts")]
        [ProducesResponseType(200, Type = typeof(ProductDto[]))]
        [ResponseCache(Duration = 2)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogTrace("This is a trace message");
            _logger.LogDebug("This is a dbg message");
            _logger.LogInformation("This is a Info message");
            _logger.LogWarning("This is a Warning message");

            try
            {
                throw new DivideByZeroException("Log exception sample...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "This is a Error message");
            }

            _logger.LogCritical("This is a FATAL message");


            Product[] products = await _productsRepository.GetAll();
            foreach (var item in products)
            {
                item.Price = _currencyService.Change(item.Price, "USD", "NIS");
            }
            return Ok(products.Select(x => _mapper.Map<ProductDto>(x)).ToArray());
        }


        [HttpGet("Search", Name = "SearchProducts")]
        [ResponseCache(Duration = 50, VaryByQueryKeys = new string[] { "*" })]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchProductDto model)
        {
            var searchModel = _mapper.Map<SearchProductModel>(model);
            var result = await _productsRepository.SearchProduct(searchModel);
            return Ok(result);
        }

        

        [HttpPut("{id}", Name = "UpdateProduct")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(409, Type = typeof(ProductDto))]
        [ServiceFilter(typeof(AddCreatedByActionFilter<ProductDto>))]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var copy = dto with { Id = id };
            var productAfterUpdate = await _productsRepository.Update(_mapper.Map<Product>(copy));
            return Ok(productAfterUpdate);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(CourseApiExceptionActionFilter))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var p = await _productsRepository.Delete(id);
            return Ok(p);
        }

        [HttpPost(Name = nameof(AddProduct))]
        [ServiceFilter(typeof(CourseApiExceptionActionFilter))]
        public async Task<IActionResult> AddProduct(ProductDto p)
        {
            var productAfterInsert = await _productsRepository.Insert(_mapper.Map<Product>(p));
            return Created($"/api/products/{productAfterInsert.Id}", _mapper.Map<ProductDto>(productAfterInsert));
        }

    }
}

