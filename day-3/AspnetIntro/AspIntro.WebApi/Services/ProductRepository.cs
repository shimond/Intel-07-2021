using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.DataContext;
using AspIntro.WebApi.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Services
{
    public class ProductRepository : IProductsRepository
    {
        private readonly IntelCourseDbContext _dbContext;
        private readonly ILogger _logger;

        public ProductRepository(IntelCourseDbContext dbContext, ILoggerFactory factory)
        {
            _dbContext = dbContext;
            this._logger = factory.CreateLogger("RepositoryLogger");

        }

        public async Task<Product> Delete(int id)
        {
            var productToDelete = await GetById(id);
            _dbContext.Products.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();

            //_dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged);
            //_dbContext.Entry(new Product { Id = id }).State = EntityState.Deleted;
            //await _dbContext.SaveChangesAsync();

            return productToDelete;
        }

        public async Task<Product[]> GetAll()
        {
            _logger.LogInformation(RestEventIds.GET_REQUEST, "Get All products from store");
            var result = await this._dbContext.Products.ToArrayAsync();
            _logger.LogInformation(RestEventIds.GET_REQUEST, "Get All products from store Finished");
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            using (_logger.BeginScope(new {ProductId= id }))
            {
                _logger.LogInformation(RestEventIds.GET_REQUEST, "Get product By Id started");

                var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
                _logger.LogInformation(RestEventIds.GET_REQUEST, "Get product By Id Get from db");
                if (product == null)
                {
                    throw new CourseApiException(ApiExceptionCodes.NotFound);
                }
                _logger.LogInformation(RestEventIds.GET_REQUEST, "Get product By Id Finished");

                return product;
            }
        }

        public async Task<Product> Insert(Product p)
        {
            _dbContext.Products.Add(p);
            await _dbContext.SaveChangesAsync();
            return p;
        }

        public async Task<Product[]> SearchProduct(SearchProductModel model)
        {
            var res = await _dbContext.Products.Where(x => x.ProductName.Contains(model.Name)
                                                            && x.Price <= model.MaxPrice
                                                            && x.Price >= model.MinPrice).ToArrayAsync();
            return res;
        }

        public async Task<Product> Update(Product p)
        {
            // _dbContext.Entry(p).State = EntityState.Modified;
            var product = await GetById(p.Id);
            product.Price = p.Price;
            product.ProductName = p.ProductName;
            product.ExpireDate = p.ExpireDate;
            product.CreatedBy = p.CreatedBy;
            await _dbContext.SaveChangesAsync();
            return product;


        }
    }
}
