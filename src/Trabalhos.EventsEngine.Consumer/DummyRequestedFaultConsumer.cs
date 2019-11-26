using MassTransit;
using Serilog;
using System;
using System.Threading.Tasks;
using Trabalhos.EventsEngine.Messages;

namespace Trabalhos.EventsEngine.ConsumerExample
{
    public class DummyRequestedFaultConsumer : IConsumer<Fault<IDummyRequest>>
    {
        public async Task Consume(ConsumeContext<Fault<IDummyRequest>> context)
        {
            try
            {
                await Task.Run(() => Log.Information("Im here now"));
                await Task.Run(() => Log.Information("Succesfully processed Record in fault queue"));
            }
            catch (Exception e)
            {
                //await Task.Run(() => Log.Information("I have errored and will retry"));
                //await context.Defer(TimeSpan.FromSeconds(100));
                await context.Redeliver(TimeSpan.FromSeconds(10));
            }
        }
    }
}
