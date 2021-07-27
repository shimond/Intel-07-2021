using AspIntro.WebApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Validations
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class ProductComplexValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var productDto = value as ProductDto;
            if (productDto == null)
            {
                return false;
            }
            return productDto.Price > 10 && productDto.ExpireDate < DateTime.Now;
        }

    }
}
