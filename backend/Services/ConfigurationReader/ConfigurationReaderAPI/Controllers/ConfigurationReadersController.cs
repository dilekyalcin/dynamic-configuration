using ConfigurationReaderAPI.Business;
using ConfigurationReaderAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationReaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationReadersController : ControllerBase
    {
        private readonly IConfigurationService _service;

        public ConfigurationReadersController(IConfigurationService service)
        {
            _service = service;
        }
        [HttpGet("all-configs")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var configs = await _service.GetAllConfigurationsAsync();
                return Ok(configs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string applicationName, [FromQuery] string? nameFilter)
        {
            try
            {
                var configs = await _service.GetConfigurationsAsync(applicationName);

                if (!string.IsNullOrEmpty(nameFilter))
                {
                    configs = configs
                        .Where(c => c.ApplicationName.ToLower().Contains(nameFilter.ToLower()))
                        .ToList();
                }

                return Ok(configs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfigurationItem item)
        {
            try
            {
                await _service.AddConfigurationAsync(item);
                return Created("", item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ConfigurationItem item)
        {
            try
            {
                if (id != item.Id)
                    return BadRequest("ID mismatch");

                await _service.UpdateConfigurationAsync(item);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
