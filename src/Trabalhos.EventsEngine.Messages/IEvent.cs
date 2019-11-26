using System;

namespace Trabalhos.EventsEngine.Messages
{
    public interface IEventMessage
    {
        DateTime Created { get; }

        Guid ForEmployee { get; }
    }
}
