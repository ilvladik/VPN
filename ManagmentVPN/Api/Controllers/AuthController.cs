using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Services;
using System;

namespace Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase, IDisposable
    {
        private readonly IUserService _userService;

        private readonly ILogger<AuthController> _logger;
        private bool disposedValue;

        public AuthController(IUserService _userservice, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userService = _userservice;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([Bind("Login,Password")] UserDto userDto)
        {
            var user = await _userService.GetByLoginAsync(userDto.Login);
            if (user == null || user.Password != userDto.Password)
            {
                return BadRequest("Неправильный логин и пароль");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            var jwt = new JwtSecurityToken
                (
                    issuer: "ManagmentVPN",
                    audience: "ManagmentVPN",
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey")), SecurityAlgorithms.HmacSha256)
                );
            var token =  new JwtSecurityTokenHandler().WriteToken(jwt);
            Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            return Ok();
        }

        

        [HttpPost("/register")]
        public async Task<IActionResult> Register([Bind("Login,Password")] UserDto userDto)
        {
            var user = await _userService.GetByLoginAsync(userDto.Login);
            if (user != null)
            {
                return BadRequest("пользователь с таким логином уже существует");
            }
            await _userService.CreateAsync(userDto);
            return Ok();
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return Ok();
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
