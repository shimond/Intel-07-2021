using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models
{
    public class SearchProductModel
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Name{ get; set; }
    }
}
