using Autoservice.BLL.DTO;
using Autoservice.BLL.Services;
using Autoservice.BLL.Services.Interfaces;
using Autoservice.BLL.DTO.HelpDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Autoservice.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _service;

        public CarController(ICarService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _service.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _service.GetByIdAsync(id);
            return car == null ? NotFound() : Ok(car);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CarDto dto)
        {
            if (id != dto.CarId)
                return BadRequest("ID mismatch.");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _service.GetByIdAsync(id);
            if (car == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("by-brand/{brand}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByBrand(string brand) => Ok(await _service.GetCarsByBrandAsync(brand));

        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] CarQueryParameters parameters)
        {
            var result = await _service.GetPagedAsync(parameters);
            return Ok(result);
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            string test = null;
            var length = test.Length; 

            return Ok();
        }
    }

}
