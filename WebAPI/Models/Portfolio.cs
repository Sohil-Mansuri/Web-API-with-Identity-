using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserID { get; set; } = string.Empty;
        public int StockID { get; set; }
        public AppUser? AppUser { get; set; }
        public Stock? Stock { get; set; } = null;

    }
}
