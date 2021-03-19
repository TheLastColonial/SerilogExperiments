namespace SerilogExperiments
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Program
    {
        private static string Application = "Serilog Experiments";
        private static string Version = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName().Version.ToString();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty(nameof(Application), Application)
                .Enrich.WithProperty(nameof(Version), Version)
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                Log.Information($"{Application} - {Version} - Starting...");
                CreateHostBuilder(args).Build().Run();
                Log.Information($"{Application} - {Version} -  Stopping...");
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{Application} - {Version} Failed to start correctly!!");
                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(
                (context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty(nameof(Application), Application)
                    .Enrich.WithProperty(nameof(Version), Version)
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
