using Microsoft.AspNetCore.Mvc;
using Inventory.Entities;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        public static List<Entities.Inventory> Inventories { get; set; } = new List<Entities.Inventory>() {
            new Entities.Inventory(){
                Id = 1,
                CategoryId = 1,
                Name = "Test1",
                Description = "Test",
            },
            new Entities.Inventory(){
                Id = 2,
                CategoryId = 2,
                Name = "Test2",
                Description = "Test",
            },
            new Entities.Inventory(){
                Id = 3,
                CategoryId = 3,
                Name = "Test3",
                Description = "Test",
            },
            new Entities.Inventory(){
                CategoryId = 4,
                Id = 4,
                Name = "Test4",
                Description = "Test",
            },
            new Entities.Inventory(){
                Id = 5,
                CategoryId = 5,
                Name = "Test5",
                Description = "Test",
            },
        };

        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "get-all-inventory")]
        public async Task<IEnumerable<Entities.Inventory>> Get()
        {
            HttpClientApi.HttpClientApi httpClientApi = new HttpClientApi.HttpClientApi("https://localhost:7098/", "Category");
            var categories = await httpClientApi.GetAllCategory();

            return Inventories
            .ToArray();
        }
    }
}