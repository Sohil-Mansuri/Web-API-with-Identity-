using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string Domain { get; set; } = string.Empty;

        public int Score { get; set; } = 1;

        public List<Portfolio> Portfolios { get; set; } = [];
    }
}
