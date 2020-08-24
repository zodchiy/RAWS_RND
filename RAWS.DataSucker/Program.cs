using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RAWS.Common;

namespace RAWS.DataSucker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    RabbitMqConnectionOptions options = configuration.GetSection("RabbitMqConnection").Get<RabbitMqConnectionOptions>();
                    services.AddSingleton(options);
                    services.AddHostedService<Worker>();
                });
    }
}
