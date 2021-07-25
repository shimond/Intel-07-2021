using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Contracts
{
    public interface ICurrencyService
    {
        double Change(double price, string codeFrom, string codeTo);
    }
}
