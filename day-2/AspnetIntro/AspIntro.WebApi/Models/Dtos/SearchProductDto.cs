using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models.Dtos
{
    public class SearchProductDto
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Name{ get; set; }

        [FromHeader(Name="my-header-value")]
        public string FromOther { get; set; }
    }
}
