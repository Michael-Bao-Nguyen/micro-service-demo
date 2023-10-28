using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Catalog.Services
{
    public interface IRabbitMqService
    {
        IConnection CreateChannel();
    }

    public class RabbitMQService : IRabbitMqService
    {
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
