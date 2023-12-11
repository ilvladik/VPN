using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Dtos.Server;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("servers")]
    [Authorize(Roles = "ADMIN")]
    public class ServerController : ControllerBase, IDisposable
    {
        private readonly IServerService _serverService;

        private readonly ILogger<AuthController> _logger;
        private bool disposedValue;

        public ServerController(IServerService _serverService, ILogger<AuthController> logger)
        {
            _logger = logger;
            _serverService = _serverService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var server = await _serverService.GetAllAsync();
            return new JsonResult(server);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServerDtoAddRequest serverDtoAddRequest)
        {
            try
            {
                await _serverService.AddAsync(serverDtoAddRequest);
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _serverService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/allow-create")]
        public async Task<IActionResult> AllowCreate(Guid id) 
        {
            await _serverService.ChangeStateAsync(id, Domain.Entities.ServerState.CREATE_ALLOWED);
            return Ok();
        }

        [HttpPost("{id}/allow-get")]
        public async Task<IActionResult> AllowGet(Guid id)
        {
            await _serverService.ChangeStateAsync(id, Domain.Entities.ServerState.GET_ALLOWED);
            return Ok();
        }

        [HttpPost("{id}/forbid")]
        public async Task<IActionResult> Forbid(Guid id)
        {
            await _serverService.ChangeStateAsync(id, Domain.Entities.ServerState.FORBIDDEN);
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
