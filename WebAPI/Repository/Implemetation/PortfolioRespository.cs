using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.DTO;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implemetation
{
    public class PortfolioRespository : IPortfolioRepository
    {
        private readonly SohilTestContext _context;
        public PortfolioRespository(SohilTestContext context)
        {
            _context = context;   
        }

        public async Task<bool> AddStock(Stock stock, AppUser user)
        {
            await _context.Portfolios.AddAsync(new Portfolio
            {
                StockID = stock.ID,
                AppUserID = user.Id
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Portfolio?> Delete(string symbol, AppUser user)
        {
           var portfolio = _context.Portfolios.FirstOrDefault(p => p.Stock != null && p.Stock.Symbol == symbol && p.AppUserID == user.Id);
            if (portfolio == null) return null;
            _context.Portfolios.Remove(portfolio);

            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<StockDto>> GetUserStocks(AppUser? user)
        {
            if (user == null) return new List<StockDto>();

            List<Stock?> stocks = await _context.Portfolios.Where(p => p.AppUserID == user.Id).Select(portFolio => portFolio.Stock).ToListAsync();

            return stocks.Select(s => s?.ToStockDTO() ?? new()).ToList();
        }
    }
}
