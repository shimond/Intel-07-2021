using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Services
{

    public class CurrencyService : ICurrencyService
    {
        public double Change(double price, string codeFrom, string codeTo)
        {
            return price * 4.1;
        }
    }
}
