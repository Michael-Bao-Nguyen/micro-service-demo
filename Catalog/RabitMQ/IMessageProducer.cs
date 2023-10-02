namespace Catalog.RabitMQ
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
