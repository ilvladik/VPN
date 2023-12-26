using ManagmentVPN.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentVPN.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("servers")]
    [Authorize(Roles = "ADMIN")]
    public class ServerController : ControllerBase, IDisposable
    {
        private readonly IServerService _serverService;

        private bool disposedValue;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var server = await _serverService.GetAllAsync();
            return Ok(server);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string networkId, string apiPort, string apiPrefix)
        {
            try
            {
                await _serverService.AddAsync(networkId, apiPort, apiPrefix);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serverService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPost("{id}/enable")]
        public async Task<IActionResult> Enable(Guid id)
        {
            await _serverService.EnableAsync(id);
            return Ok();
        }

        [HttpPost("{id}/disable")]
        public async Task<IActionResult> Disable(Guid id)
        {
            await _serverService.DisableAsync(id);
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
