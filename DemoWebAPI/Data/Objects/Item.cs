using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoWebApi.Data.Objects
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public int Count { get; set; }

        public Item(string name, string description, string price, int count, string? createdBy)
        {
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = DateTime.Now;
            LastUpdated = DateTime.Now;
            CreatedBy = createdBy;
            Count = count;
        }
    }
}
