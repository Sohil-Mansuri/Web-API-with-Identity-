using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDTO(this Stock stock)
        {
            return new StockDto
            {
                ID = stock.ID,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Created = stock.Created.ToString("dddd, dd MMMM yyyy"),
            };
        }

        public static Stock ToStockFromCreateStockRequestDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }

        public static void UpdateStock(this Stock stock, CreateStockRequestDto stockDto)
        {
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;
        }
    }
}
