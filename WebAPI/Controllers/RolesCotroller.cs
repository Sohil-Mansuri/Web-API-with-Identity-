using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesCotroller : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _rollManager;
        public RolesCotroller(RoleManager<IdentityRole> rollManager)
        {
           _rollManager = rollManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] string? role)
        {
            try
            {
                if (string.IsNullOrEmpty(role)) return BadRequest(role);

                var newRole = new IdentityRole
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };

                var roleExist = await _rollManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    var result = await _rollManager.CreateAsync(newRole);
                    if (result.Succeeded)
                    {
                        return Ok($"{role} role is created");
                    }
                    else
                    {
                        return StatusCode(500, result.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, $"{role} already exist");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            
        }
    }
}
