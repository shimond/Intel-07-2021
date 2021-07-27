using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models.Config
{
    public class RedisConfiguration
    {
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
