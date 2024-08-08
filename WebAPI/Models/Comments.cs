namespace WebAPI.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int? StockID { get; set; }
        public Stock? Stock { get; set; }
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
