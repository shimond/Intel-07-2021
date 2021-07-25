using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace AspIntro.WebApi.ConfigurationProviders
{
    public class MyCustomConfigurationProvider : ConfigurationProvider
    {
        public MyCustomConfigurationProvider(string connectionString)
        {
            throw new Exception("Halas Exception.");

            int count = 0;
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += (_, _) => {
                count += 10;
                Console.WriteLine("Print it! " + count);
                Data = new Dictionary<string, string>
                {
                    ["Redis:ConnectionString"] = connectionString + count,
                    ["Redis:UserName"] = "UserNameFromCustomProvider" + count,
                    ["Redis:Password"] = "PasswordFromCustomProvider" + count,
                };
                this.OnReload();
            };
            timer.Start();
        }

        public override void Load()
        {
            Data = new Dictionary<string, string>
            {
                ["Redis:ConnectionString"] = "ConnectionStringFromCustomProvider",
                ["Redis:UserName"] = "UserNameFromCustomProvider",
                ["Redis:Password"] = "PasswordFromCustomProvider",
            };
        }
    }

    public class MyCustomConfigurationSource : IConfigurationSource
    {
        private string _conStr;

        public MyCustomConfigurationSource(string connectionString)
        {
            _conStr = connectionString;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyCustomConfigurationProvider(_conStr);
        }
    }
}
