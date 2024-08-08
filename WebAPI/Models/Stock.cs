using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Stock
    {
        public int ID { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public List<Comments> Comments { get; set; } = new();

        public List<Portfolio> Portfolios { get; set; } = [];
    }

}
