using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Models.Dtos
{
    public record ProductDto : ICreatedBy
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        public DateTime ExpireDate { get; init; }
        public double Price { get; init; }
    }

    public record ICreatedBy
    {
        public string CreatedBy { get; init; }
    }

    //public record ProductDto(int Id, string Name, DateTime ExpireDate, double Price);




}
