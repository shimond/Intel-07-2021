using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpireDate { get; set; }
        public double Price { get; set; }
        //public string Producer { get; set; }
    }
}
