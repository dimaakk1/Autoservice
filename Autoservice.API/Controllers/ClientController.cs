using Autoservice.BLL.DTO;
using Autoservice.BLL.Services;
using Autoservice.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Autoservice.BLL.DTO.HelpDTO;
using Microsoft.AspNetCore.Authorization;

namespace Autoservice.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var client = await _service.GetByIdAsync(id);
            if (client == null)
                return NotFound($"Client with ID {id} not found.");

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(dto);
            return StatusCode(201);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("with-records")]
        public async Task<IActionResult> GetClientsWithRecords() => Ok(await _service.GetClientsWithRecordsAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> Search([FromQuery] ClientQueryParameters parameters)
        {
            var result = await _service.GetPagedAsync(parameters);
            return Ok(result);
        }
    }

}
