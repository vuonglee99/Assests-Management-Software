using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GSoft.AbpZeroTemplate.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel(opt => opt.AddServerHeader = false)
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseUrls("http://localhost:5000", "http://192.168.1.135:5000")
                .UseUrls("http://localhost:5000")
                .UseIISIntegration()
                .UseStartup<Startup>();
        }

        //.UseUrls("http://localhost:5000", "http://192.168.0.101:5000")
        //.UseUrls("http://localhost:5000")
    }
}
