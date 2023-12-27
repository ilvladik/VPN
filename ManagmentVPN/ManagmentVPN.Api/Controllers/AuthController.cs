using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Entities;
using ManagmentVPN.Application.Dtos;
using System.Reflection.Metadata.Ecma335;


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
        public async Task<IActionResult> Register([Bind("Login,Password")] UserDto user)
        {
            User? userResult = await _unitOfWork.Users.GetByLoginAsync(user.Login);
            if (userResult is not null)
                return BadRequest("User already exists");
            User? newUser = new User
            {
                Id = Guid.NewGuid(),
                Login = user.Login,
                Password = user.Password,
                Role = UserRole.USER,
                VpnAccessMode = VpnAccessMode.ALLOWED
            };
            if (ModelState.IsValid)
            {
                await _unitOfWork.Users.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(newUser.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([Bind("Login,Password")] UserDto user)
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

            return Ok(userResult.Id);
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
