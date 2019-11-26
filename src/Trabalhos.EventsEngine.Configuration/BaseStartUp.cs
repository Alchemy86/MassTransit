using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Trabalhos.EventsEngine
{
    public abstract class BaseStartup
    {
        public void ConfigureHostConfiguration(IConfigurationBuilder cb)
        {
            // https://github.com/aspnet/Hosting/issues/1440 - Override hosting env for generic host
            cb.AddEnvironmentVariables("ASPNETCORE_");
            cb.SetBasePath(Directory.GetCurrentDirectory());
        }

        public abstract void ConfigureAppConfiguration(HostBuilderContext hostBuilder, IConfigurationBuilder configBuilder);

        public abstract void ConfigureServices(HostBuilderContext hostBuilder, IServiceCollection services);

        public abstract void ConfigureLogging(HostBuilderContext hostBuilder, ILoggingBuilder lb);
    }
}
