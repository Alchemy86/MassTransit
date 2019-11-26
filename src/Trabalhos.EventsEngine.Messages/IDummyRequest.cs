using System.Collections.Generic;

namespace Trabalhos.EventsEngine.Messages
{
    public interface IDummyRequest : IEventMessage
    {
        IList<IDummyData> requestedData { get; }
    }
}
