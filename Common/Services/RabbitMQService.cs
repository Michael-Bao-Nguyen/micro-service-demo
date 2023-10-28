using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public interface IRabbitMQService {
        IConnection CreateChannel();
    }
    public class RabbitMQService : IRabbitMQService
    {

        public IConnection CreateChannel()
        {
            throw new NotImplementedException();
        }
    }
}
