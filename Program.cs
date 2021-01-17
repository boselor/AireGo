using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (args.Length > 0)
                    {
                        if (args[0] != null && args[0].Length > 0)
                        {
                            Console.WriteLine("Use port {0}", args[0]);
                            webBuilder
                            .UseUrls($"http://*:{args[0]}")
                            .UseStartup<Startup>();
                        }
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                });
    }
}
