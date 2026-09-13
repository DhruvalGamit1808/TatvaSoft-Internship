using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (request.Username == "admin" &&
                request.Password == "admin123")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("ThisIsMyVerySecureSecretKey1234567890"));

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials);

                return Ok(new
                {
                    message = "Login successful",
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}