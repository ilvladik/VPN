using ManagmentVPN.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentVPN.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("keys")]
    public class KeyController : ControllerBase, IDisposable
    {
        private readonly IKeyService _keyService;
        private bool disposedValue;

        public KeyController(IKeyService keyService)
        {
            _keyService = keyService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _keyService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVpnKey(Guid id)
        {
            try
            {
                var key = await _keyService.GetByUserIdAsync(id);
                return Ok(key);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
