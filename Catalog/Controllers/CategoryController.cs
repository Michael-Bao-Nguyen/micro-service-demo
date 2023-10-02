using Catalog.Entities;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IPublishEndpoint _publishEndpoint;
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
            )
        {
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
        public IActionResult AddNew( [FromBody] Category category) {
            categories.Add(category);
            _publishEndpoint.Publish(category);
            return Ok();
        }

    }
}