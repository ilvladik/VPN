using ManagmentVPN.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagmentVPN.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase, IDisposable
    {
        private readonly IUserService _userService;
        private readonly IKeyService _keyService;
        private bool disposedValue;

        public UserController(IUserService userService, IKeyService keyService)
        {
            _userService = userService;
            _keyService = keyService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok((await _userService.GetAllAsync())
                .Select(u => 
                    new { 
                        u.Id, u.Login, u.Role, u.VpnAccessMode, KeyId = u.Key?.Id
                    }));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/allow")]
        public async Task<IActionResult> ChangeAccessToAllowed(Guid id)
        {
            await _userService.AllowAsync(id);
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/forbid")]
        public async Task<IActionResult> ChangeAccessToForbid(Guid id)
        {
            await _userService.ForbidAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVpn(Guid id)
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
                    _userService.Dispose();
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
