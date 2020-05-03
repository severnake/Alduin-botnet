using Alduin.Server.Modules;
using Alduin.Web.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace Alduin.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            appsettingsModel appsettings = JsonConvert.DeserializeAnonymousType(ServerFileManager.FileReader(GetPathes.Get_SolutionMainPath() + "/Alduin.Web/appsettings.json"), new appsettingsModel());
            if (appsettings.Tor.RunItStart)
            {
                Thread thr = new Thread(new ThreadStart(ConfigTor.StartTor));
                Thread thrCD = new Thread(new ThreadStart(CloseDetector.Detecter));
                thr.Start();
                thrCD.Start();
            } 
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory());
                    cfg.AddJsonFile("appsettings.json", false, true);
                    cfg.AddEnvironmentVariables();
                }).UseStartup<Startup>();
    }
}
