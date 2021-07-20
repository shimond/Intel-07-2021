using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Contracts
{
    public interface IProductsRepository
    {
        Task<Product[]> GetAll();

        Task<Product> GetById(int id);


        Task<Product> Update(Product p);

        Task<Product[]> SearchProduct(SearchProductModel model);

    }
}
