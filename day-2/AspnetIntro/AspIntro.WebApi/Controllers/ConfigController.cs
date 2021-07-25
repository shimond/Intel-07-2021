using AspIntro.WebApi.ActionFilters;
using AspIntro.WebApi.Contracts;
using AspIntro.WebApi.Exceptions;
using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Config;
using AspIntro.WebApi.Models.Dtos;
using AspIntro.WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IConfiguration _configuration;
        private readonly RedisConfiguration _redisConfig;

        public ConfigController(
            ILogger<ConfigController> logger, 
            IConfiguration configuration,
            MySingleService mySingleService,
            IOptionsSnapshot<RedisConfiguration> redisConfigConfig // by scope
            )
        {
            _logger = logger;
            _configuration = configuration;
            _redisConfig = redisConfigConfig.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_redisConfig);
        }



    }
}

