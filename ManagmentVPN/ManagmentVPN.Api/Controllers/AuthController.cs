using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Entities;


namespace ManagmentVPN.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([Bind("Login,Password")] User user)
        {
            User? userResult = await _unitOfWork.Users.GetByLoginAsync(user.Login);
            if (userResult is not null)
                throw new Exception("User already exists");
            userResult.Id = Guid.NewGuid();
            userResult.Role = UserRole.USER;
            if (ModelState.IsValid)
            {
                await _unitOfWork.Users.AddAsync(userResult);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([Bind("Login,Password")] User user)
        {
            User? userResult = await _unitOfWork.Users.GetByLoginAsync(user.Login);
            if (userResult is null || user.Password != user.Password)
            {
                return NotFound();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userResult.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userResult.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);
            await Response.HttpContext.SignInAsync(claimsPricipal);

            return Ok();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await Response.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
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
