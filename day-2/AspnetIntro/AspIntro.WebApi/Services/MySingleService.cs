using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Config;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Services
{

    public class MySingleService 
    {
        private readonly ILogger<MySingleService> _logger;
        private RedisConfiguration _redisConfig;

        public MySingleService(
            ILogger<MySingleService> logger,
            IOptionsMonitor<RedisConfiguration> redisOptionsMoitor)
        {
            _logger = logger;
            _logger.LogInformation("MySingleService Ctor");
            this._redisConfig = redisOptionsMoitor.CurrentValue;
            redisOptionsMoitor.OnChange(currentConfig => {
                _redisConfig = currentConfig;
                _logger.LogInformation("Config Changed!");
                Console.WriteLine("Config changed.... CW");
                //reload..
            });
        }
       
    }
}
