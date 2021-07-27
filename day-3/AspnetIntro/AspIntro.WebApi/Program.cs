using AspIntro.WebApi.ConfigurationProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("serilog.json").Build();
                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Wowwww sorry  - " + ex.Message);
                Log.Logger.Fatal(ex, "Application fatal error");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("redis.json", true, true);
                    var tempConfiguration = config.Build();
                    config.Add(new MyCustomConfigurationSource(tempConfiguration["ConnectionStrings:myDb"]));
                })
            //.ConfigureLogging(logging =>
            //{
            //    //logging.AddProvider
            //    //logging.ClearProviders(); // Console, Debug, EvetLog (windows)
            //    logging.AddSeq();

            //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
