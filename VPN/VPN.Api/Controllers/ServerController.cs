using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.Dtos.Server;
using VPN.Application.Services;

namespace VPN.Api.Controllers
{
    [ApiController]
    [Route("api/servers")]

    public class ServerController : ControllerBase, IDisposable
    {
        private readonly IServerService _serverService;

        public ServerController(IServerService serverService, ILogger<ServerController> logger)
        {
            _serverService = serverService;
            _logger = logger;
        }

        private readonly ILogger<ServerController> _logger;
        private bool disposedValue;

        [HttpPost]
        public async Task<Response<ServerDtoResponse>> Post(ServerDtoAddRequest serverDtoAddRequest)
        {
            try
            {
                ServerDtoResponse serverDtoResponse = await _serverService.AddAsync(serverDtoAddRequest);
                _logger.LogInformation("Server Added");
                return new Response<ServerDtoResponse>() { IsSuccess = true, Dto = serverDtoResponse, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<ServerDtoResponse>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
        }
        [HttpGet]
        public async Task<Response<IEnumerable<ServerDtoResponse>>> GetAll()
        {
            try
            {
                IEnumerable<ServerDtoResponse> serverDtoResponse = await _serverService.GetAllAsync();
                _logger.LogInformation("Get All Servers");
                return new Response<IEnumerable<ServerDtoResponse>>() { IsSuccess = true, Dto = serverDtoResponse, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ServerDtoResponse>>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
        }

        [HttpDelete("{id}")]
        public async Task<Response<ServerDtoResponse>> Delete(Guid id)
        {
            try
            {
                await _serverService.DeleteAsync(id);
                _logger.LogInformation("Server Deleted");
                return new Response<ServerDtoResponse>() { IsSuccess = true, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<ServerDtoResponse>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
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
