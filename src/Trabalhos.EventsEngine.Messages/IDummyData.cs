namespace Trabalhos.EventsEngine.Messages
{
    public interface IDummyData : IEventMessage
    {
        string Name { get; }

        decimal Price { get; }
    }
}
