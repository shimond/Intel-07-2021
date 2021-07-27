using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Services
{
    public class MemoryProductRepository : IProductsRepository
    {
        private const string PRODUCTS_CACHE_KEY = "products";
        private readonly IDistributedCache _distributedCache;

        public MemoryProductRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<Product> Delete(int id)
        {
            var all = (await this.GetAll()).ToList();
            var product = all.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new CourseApiException(ApiExceptionCodes.NotFound);
            }
            all.Remove(product);
            await _distributedCache.SetStringAsync(PRODUCTS_CACHE_KEY, JsonConvert.SerializeObject(all));
            return product;
        }

        public async Task<Product[]> GetAll()
        {
            var productsString = await _distributedCache.GetStringAsync(PRODUCTS_CACHE_KEY);
            if (string.IsNullOrEmpty(productsString))
            {
                var products = new Product[] {
                new Product { Id = 1, ProductName = "Bamba", ExpireDate = DateTime.Now.AddDays(10), Price = 12.6 },
                new Product { Id = 2, ProductName = "Bisli", ExpireDate = DateTime.Now.AddDays(140), Price = 19.5 },
                new Product { Id = 3, ProductName = "Kefli", ExpireDate = DateTime.Now.AddDays(50), Price = 20.6 } };
                await _distributedCache.SetStringAsync(PRODUCTS_CACHE_KEY, JsonConvert.SerializeObject(products));
            }
            var stringFromCache = await _distributedCache.GetStringAsync(PRODUCTS_CACHE_KEY);
            return JsonConvert.DeserializeObject<Product[]>(stringFromCache);
        }

        public async Task<Product> GetById(int id)
        {
            var all = await GetAll();
            var product = all.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new CourseApiException(ApiExceptionCodes.NotFound);
            }
            return product;
        }

        public async Task<Product> Insert(Product p)
        {
            var all = (await this.GetAll()).ToList();
            var lastId =  all.Select(x => x.Id).Last();
            p.Id = lastId + 1;
            all.Add(p);
            await _distributedCache.SetStringAsync(PRODUCTS_CACHE_KEY, JsonConvert.SerializeObject(all));
            return p;
        }

        public async Task<Product[]> SearchProduct(SearchProductModel model)
        {
            var products = await GetAll();
            return products.Where(x =>
            x.ProductName.Contains(model.Name)
            && x.Price <= model.MaxPrice
            && x.Price >= model.MinPrice
            ).ToArray();
        }

        public async Task<Product> Update(Product p)
        {
            var products = await this.GetAll();
            var product = products.FirstOrDefault(x => x.Id == p.Id);
            if (product == null)
            {
                throw new CourseApiException(ApiExceptionCodes.NotFound);
            }
            product.Price = p.Price;
            product.ProductName = p.ProductName;
            product.ExpireDate = p.ExpireDate;
            await _distributedCache.SetStringAsync(PRODUCTS_CACHE_KEY, JsonConvert.SerializeObject(products));
            return product;


        }
    }
}
