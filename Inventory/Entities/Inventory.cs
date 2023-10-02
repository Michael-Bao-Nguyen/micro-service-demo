namespace Inventory.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }    
        public string? Description { get; set; }
    }
}
