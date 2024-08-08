using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.DTO;
using WebAPI.Helper;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implemetation
{
    public class StockRepository : IStockRepository
    {
        private readonly SohilTestContext _context;
        public StockRepository(SohilTestContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock?> DeleteByIdAsync(int id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if (stock is null) return null;

            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            //include to with referencing
            var stock = _context.Stock.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                //var result = await _context.Stock.FromSql($"SELECT * FROM Stocks WHERE LOWER(Symbol) = LOWER({queryObject.Symbol})").ToListAsync();
                stock = stock.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy) && string.Equals(queryObject.SortBy, "Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stock = queryObject.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await stock.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(s => s.Comments).FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<Stock?> UpdateAsync(int id, CreateStockRequestDto stockDto)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock is null) return null;

            stock.UpdateStock(stockDto);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateCompanyNameAsync(int id, string companyName)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock is null) return null;

            stock.CompanyName = companyName;
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> GetBySymbol(string? symbol)
        {
            if (symbol is null) return null;
            var stock = await _context.Stock.FirstOrDefaultAsync(stock => stock.Symbol == symbol);
            if (stock is null) return null;
            return stock;
        }
    }
}
