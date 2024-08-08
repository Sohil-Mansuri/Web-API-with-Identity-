using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this AppUser appUser)
        {
            return new UserDto
            {
                UserName = appUser.UserName ?? string.Empty,
                Email = appUser.Email ?? string.Empty,
            };
        }
    }
}
