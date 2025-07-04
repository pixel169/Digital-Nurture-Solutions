using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailInventory.Models
{
    public class StockLevel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int MinimumStock { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public bool IsLowStock => Quantity <= MinimumStock;
    }
}