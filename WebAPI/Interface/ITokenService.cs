using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
