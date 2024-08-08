using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.DTO;
using WebAPI.Helper;
using WebAPI.Mapper;
using WebAPI.Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var stocks = await _stockRepository.GetAllAsync(queryObject);
            var stockDtos = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock is null) return NotFound();

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateStockRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(request);

            var stockDbModel = request.ToStockFromCreateStockRequestDto();
            await _stockRepository.AddAsync(stockDbModel);
            return CreatedAtAction(nameof(GetByID), new { id = stockDbModel.ID }, stockDbModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateStockRequestDto createStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.UpdateAsync(id, createStockRequestDto);
            if (stock is null) return NotFound();

            return Ok(stock.ToStockDTO());
        }

        [HttpPut("UpdateCompanyName/{id}")]
        //[Route("UpdateCompanyName/{id}")]
        public async Task<IActionResult> UpdateCompanyName([FromRoute] int id, [FromBody] string companyName)
        {
            var stock = await _stockRepository.UpdateCompanyNameAsync(id, companyName);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteByIdAsync(id);

            if (stock is null) return NotFound();

            return NoContent();
        }

        [HttpGet]
        [Route("GetBySymbol")]
        public async Task<IActionResult> GetBySymbol([FromBody] string? symbol)
        {
            var stock = await _stockRepository.GetBySymbol(symbol);

            if (stock is null) return NotFound($"{symbol} not found");

            return Ok(stock.ToStockDTO());
        }
    }
}
