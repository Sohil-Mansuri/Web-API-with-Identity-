using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository  _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //get username with calim
            //var userName = User.GetUsername();
            //var appUser2 = await _userManager.FindByNameAsync(userName);
            var appUser = await _userManager.GetUserAsync(User);
            var userStocks = await _portfolioRepository.GetUserStocks(appUser);
            return Ok(userStocks);
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddStocktoUser([FromBody] string symbol)
        {
            var stock = await _stockRepository.GetBySymbol(symbol);

            if (stock is null) return NotFound($"{symbol} stock not found");

            var appUser  = await _userManager.GetUserAsync(User);
            if (appUser is null) return Unauthorized();

            var useCurrentStocks = await _portfolioRepository.GetUserStocks(appUser);

            if (useCurrentStocks.Any(stock => stock.Symbol == symbol))
                return BadRequest($"{symbol} is already been added");

            await _portfolioRepository.AddStock(stock, appUser);

            return Ok($"{symbol} stock added");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string? symbol)
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (symbol is null || appUser is null) return BadRequest();
            var portFolio = await _portfolioRepository.Delete(symbol,appUser);
            return Ok($"{symbol} deleted");
        }
    }
}
