using MassTransit;
using Serilog;
using System.Threading.Tasks;
using Trabalhos.EventsEngine.Messages;

namespace Trabalhos.EventsEngine.ConsumerExample
{
    public class DummyRequestedConsumer : IConsumer<IDummyRequest>
    {
        public Task Consume(ConsumeContext<IDummyRequest> context)
        {
            //var publish = context.Publish<IDummyAccepted>(new { context.Message.requestedData });
            var logger = Task.Run(() => Log.Information("Processed Record: " + context.MessageId));
            //throw new System.Exception("Fluffed it");

            return Task.CompletedTask;
    
            //try
            //{
                
            //}
            //catch (Exception e)
            //{
            //    //await Task.Run(() => Log.Information("I have errored and will retry"));
            //    //await context.Defer(TimeSpan.FromSeconds(100));
            //    await context.Redeliver(TimeSpan.FromSeconds(10));
            //    return Task.CompletedTask;
            //}
        }
    }
}
