using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using Trabalhos.EventsEngine.Models;

namespace Trabalhos.EventsEngine.ConsumerExample
{
    public class Startup : BaseStartup
    {
        public override void ConfigureAppConfiguration(HostBuilderContext hostBuilder, IConfigurationBuilder configBuilder)
        {
            configBuilder.AddJsonFile(@"Config/appsettings.json", false, true);
        }

        public override void ConfigureLogging(HostBuilderContext hostBuilder, ILoggingBuilder lb)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(@"Config/serilog.json", true);

            if (hostBuilder.HostingEnvironment.IsDevelopment())
                configuration.AddUserSecrets<Startup>();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.Build())
                .CreateLogger();

            lb.AddSerilog();
        }

        public override void ConfigureServices(HostBuilderContext hostBuilder, IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<Settings>(hostBuilder.Configuration);
            services.AddSingleton<IHostedService, HostedService>();
            this.SetupMassTransit(services);
        }

        private void SetupMassTransit(IServiceCollection services)
        {
            services.AddScoped<DummyRequestedConsumer>();

            var serviceProvider = services
                .BuildServiceProvider();

            var settings = serviceProvider
                .GetRequiredService<IOptions<Settings>>()
                .Value;

            services.AddSingleton(provider => BusConfiguration.ConfigureBus(settings, (cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, settings.Rabbit.Queue, 
                    e => e.Consumer<DummyRequestedConsumer>(consumerCfg =>
                        {
                            consumerCfg.UseRetry(retryConfig =>
                                retryConfig.Interval(10, TimeSpan.FromMilliseconds(200)));
                        }));

                cfg.ReceiveEndpoint(host, settings.Rabbit.FaultQueue,
                    e => e.Consumer<DummyRequestedFaultConsumer>(consumerCfg =>
                    {
                        consumerCfg.UseRetry(retryConfig =>
                            retryConfig.Interval(1, TimeSpan.FromMilliseconds(200)));
                    }));
            }));
        }
    }
}
