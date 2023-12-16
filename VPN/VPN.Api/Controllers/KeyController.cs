using Microsoft.AspNetCore.Mvc;
using VPN.Application.Services;
using VPN.Domain.Exceptions;

namespace VPN.Api.Controllers
{
    [ApiController]
    [Route("api/keys")]

    public class KeyController : ControllerBase, IDisposable
    {
        private readonly IKeyService _keyService;
        private bool disposedValue;

        public KeyController(IKeyService keyService)
        {
            _keyService = keyService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Add(Guid serverId)
        {
            try
            {
                return Ok(await _keyService.CreateAsync(serverId));
            } 
            catch (ServerNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _keyService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _keyService.GetByIdAsync(id));
            } 
            catch (Domain.Exceptions.KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("server/{serverId}")]
        public async Task<IActionResult> GetAllByServerId(Guid serverId)
        {
            return Ok(await _keyService.GetByServerIdAsync(serverId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _keyService.DeleteAsync(id);
            return Ok();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _keyService.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
