using Catalog.Entities;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;
        public static List<Category> categories { get; set; } = new List<Category>() { 
            new Category(){ 
                Id = 1, 
                Name = "Test1",
                Description = "Test",
            },
            new Category(){
                Id = 2,
                Name = "Test2",
                Description = "Test",
            },
            new Category(){
                Id = 3,
                Name = "Test3",
                Description = "Test",
            },
            new Category(){
                Id = 4,
                Name = "Test4",
                Description = "Test",
            },
            new Category(){
                Id = 5,
                Name = "Test5",
                Description = "Test",
            },
        };

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger
            , IPublishEndpoint publishEndpoint
            , IBus bus
            )
        {
            _bus = bus;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet(Name = "get-all")]
        public IEnumerable<Category> Get()
        {
            return categories
            .ToArray();
        }
        [HttpPost("add-new-summaries")]
        public async Task<IActionResult> AddNew( [FromBody] Category category) {
            categories.Add(category);
            //Uri uri = new Uri("rabbitmq://localhost/todoQueue");
            //var endPoint = await _bus.GetSendEndpoint(uri);
            //await endPoint.Send(category);

            //_publishEndpoint.Publish(category);
            //
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "user" , Password = "password"};
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

            return Ok();
        }

    }
}