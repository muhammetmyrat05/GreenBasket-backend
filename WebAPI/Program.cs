using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Удаляем из конфигурации секции, начинающиеся с "Kestrel", чтобы не включался HTTPS по ним
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var built = config.Build().AsEnumerable()
                        .Where(kv => string.IsNullOrEmpty(kv.Key) || !kv.Key.StartsWith("Kestrel", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    config.Sources.Clear();
                    config.AddInMemoryCollection(built);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Явно слушаем HTTP на всех интерфейсах
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                });
    }
}