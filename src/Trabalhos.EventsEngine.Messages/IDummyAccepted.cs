using System.Collections.Generic;

namespace Trabalhos.EventsEngine.Messages
{
    public interface IDummyAccepted : IEventMessage
    {
        IList<IDummyData> requestedData { get; }
    }
}
