using WebAPI.DTO;
using WebAPI.Helper;
using WebAPI.Models;

namespace WebAPI.Repository.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);

        Task<Stock?> GetByIdAsync(int id);

        Task AddAsync(Stock stock);

        Task<Stock?> UpdateAsync(int id, CreateStockRequestDto stockDto);

        Task<Stock?> UpdateCompanyNameAsync(int id, string companyName);

        Task<Stock?> DeleteByIdAsync(int id);

        Task<Stock?> GetBySymbol(string? symbol);
    }
}
