using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Trabalhos.EventsEngine
{
    public class HostedService : IHostedService
    {
        private readonly IBusControl busControl;
        private readonly ILogger<HostedService> logger;

        public HostedService(IBusControl busControl, ILogger<HostedService> logger)
        {
            this.busControl = busControl;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await busControl.StartAsync(cancellationToken);
            // http://masstransit-project.com/MassTransit/troubleshooting/show-config.html - Output bus configuration
            logger.Log(LogLevel.Information, "Current Configuration");
            logger.LogInformation(JsonConvert.SerializeObject(busControl.GetProbeResult(), Formatting.Indented));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await busControl.StopAsync(cancellationToken);
        }
    }
}
