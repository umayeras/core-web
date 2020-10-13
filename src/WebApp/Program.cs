using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using WebApp.Core.Constants;
using WebApp.Core.ElasticSearch;

namespace WebApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogging();
            CreateHost(args);
        }

        #region configure logging

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            var elasticSearchSettings = configuration.GetSection(SectionNames.ElasticSearchSettings)
                .Get<ElasticSearchSettings>();
            var generalSettings = configuration.GetSection(SectionNames.GeneralSettings).Get<GeneralSettings>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Elasticsearch(ConfigureElasticSink(elasticSearchSettings.Uri, generalSettings.ApplicationName))
                .Enrich.WithProperty("Application", generalSettings.ApplicationName)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(string uri, string applicationName)
        {
            return new ElasticsearchSinkOptions(new Uri(uri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{applicationName}-log-{0:yyyy.MM.dd}",
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
            };
        }

        #endregion

        #region create host

        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.AddServerHeader = false;
                    });
                    webBuilder.UseStartup<Startup>();
                });

        #endregion
    }
}