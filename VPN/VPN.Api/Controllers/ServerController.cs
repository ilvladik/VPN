using Microsoft.AspNetCore.Mvc;
using VPN.Application.Services;
using VPN.Domain.Exceptions;

namespace VPN.Api.Controllers
{
    [ApiController]
    [Route("api/servers")]

    public class ServerController : ControllerBase, IDisposable
    {
        private readonly IServerService _serverService;
        private bool disposedValue;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }


        [HttpPost]
        public async Task<IActionResult> Add(string networkId, int apiPort, string apiPrefix)
        {
            return Ok(await _serverService.AddAsync(networkId, apiPort, apiPrefix));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _serverService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try 
            {
                return Ok(await _serverService.GetByIdAsync(id));
            }
            catch (ServerNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serverService.DeleteAsync(id);
            return Ok();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   _serverService.Dispose();
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
