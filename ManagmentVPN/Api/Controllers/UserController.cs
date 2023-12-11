using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase, IDisposable
    {
        private readonly IUserService _userService;

        private readonly ILogger<AuthController> _logger;
        private bool disposedValue;

        public UserController(IUserService _userservice, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userService = _userservice;
        }

        [Authorize(Roles="ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new JsonResult((await _userService.GetAllAsync()).Select(u => new { u.Id, u.Login, u.Role, u.VpnAccessMode}));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/enable")]
        public async Task<IActionResult> ChangeAccessToEnabled(Guid id) 
        {
            await _userService.AllowVpnAccess(id);
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/disable")]
        public async Task<IActionResult> ChangeAccessToDisable(Guid id)
        {
            await _userService.DisableVpnAccess(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVpn(Guid id)
        {
            var key = await _userService.GetVpn(id);
            if (key == null)
                return Forbid();
            return new JsonResult(key);
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
