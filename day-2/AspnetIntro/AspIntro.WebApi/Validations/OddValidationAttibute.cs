using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Validations
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class OddValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(int.TryParse(value.ToString(), out int result))
            {
                int number = result;
                return number % 2 == 1;
            }
            return false;
        }

        public OddValidationAttribute()
        {
            this.ErrorMessage = "Default error message";
        }
    }
}
