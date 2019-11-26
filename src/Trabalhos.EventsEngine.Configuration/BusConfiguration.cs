using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using Trabalhos.EventsEngine.Models;

namespace Trabalhos.EventsEngine
{
    public static class BusConfiguration
    {
        public static IBusControl ConfigureBus(Settings settings, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registration = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(settings.Rabbit.Uri, h =>
                {
                    h.Username(settings.Rabbit.Username);
                    h.Password(settings.Rabbit.Password);
                });

                cfg.UseSerilog();

                registration?.Invoke(cfg, host);
            });
        }
    }
}
