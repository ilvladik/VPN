using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Dtos.Server;
using SharedKernel;
using VPN.Application.Services;
using SharedKernel.Dtos.Key;

namespace VPN.Api.Controllers
{
    [ApiController]
    [Route("api/keys")]

    public class KeyController : ControllerBase, IDisposable
    {
        private readonly IKeyService _keyService;

        public KeyController(IKeyService keyService, ILogger<ServerController> logger)
        {
            _keyService = keyService;
            _logger = logger;
        }
        private readonly ILogger<ServerController> _logger;
        private bool disposedValue;

        [HttpPost]
        public async Task<Response<KeyDtoResponse>> Add()
        {
            try
            {
                KeyDtoResponse keyDtoResponse = await _keyService.CreateAsync();
                _logger.LogInformation("Key Created");
                return new Response<KeyDtoResponse>() { IsSuccess = true, Dto = keyDtoResponse, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<KeyDtoResponse>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
        }

        [HttpGet]
        public async Task<Response<IEnumerable<KeyDtoResponse>>> GetAll()
        {
            try
            {
                IEnumerable<KeyDtoResponse> serverDtoResponse = await _keyService.GetAllAsync();
                _logger.LogInformation("Get All Keys");
                return new Response<IEnumerable<KeyDtoResponse>>() { IsSuccess = true, Dto = serverDtoResponse, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<KeyDtoResponse>>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
        }

        [HttpGet("server/{serverId}")]
        public async Task<Response<IEnumerable<KeyDtoResponse>>> GetAllByServerId(Guid serverId)
        {
            try
            {
                IEnumerable<KeyDtoResponse> serverDtoResponse = await _keyService.GetByServerIdAsync(serverId);
                _logger.LogInformation("Get All Keys By ServerId");
                return new Response<IEnumerable<KeyDtoResponse>>() { IsSuccess = true, Dto = serverDtoResponse, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<KeyDtoResponse>>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
        }

        [HttpDelete("{id}")]
        public async Task<Response<KeyDtoResponse>> Delete(Guid id)
        {
            try
            {
                await _keyService.DeleteAsync(id);
                _logger.LogInformation("Key Deleted");
                return new Response<KeyDtoResponse>() { IsSuccess = true, Errors = new() };
            }
            catch (Exception ex)
            {
                return new Response<KeyDtoResponse>() { IsSuccess = false, Errors = new() { $"{ex.GetType()}: {ex.Message}" } };
            }
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
