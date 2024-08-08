using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Repository.Interface
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetUserStocks(AppUser? user);

        Task<bool> AddStock(Stock stock, AppUser user);

        Task<Portfolio?> Delete(string symbol, AppUser user);
    }
}
