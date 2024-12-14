using FinanceMenagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinanceMenagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private static readonly Dictionary<string, string> refreshTokens = new();

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (ValidateUserCredentials(user.Username, user.Password))
            {
                var token = GenerateJwtToken(user.Username);
                var refreshToken = GenerateRefreshToken();

                // Dodajemy Refresh Token do listy
                refreshTokens[refreshToken] = user.Username;

                return Ok(new
                {
                    Token = token,
                    RefreshToken = refreshToken
                });
            }

            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            // Sprawdzanie poprawności użytkownika
            if (!ValidateUserCredentials(request.Username, request.Password))
            {
                return Unauthorized("Invalid username or password.");
            }

            // Sprawdzanie poprawności Refresh Token
            if (string.IsNullOrEmpty(request.RefreshToken) ||
                !refreshTokens.TryGetValue(request.RefreshToken, out var storedUsername) ||
                storedUsername != request.Username)
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            // Generowanie nowych tokenów
            var newAccessToken = GenerateJwtToken(request.Username);
            var newRefreshToken = GenerateRefreshToken();

            // Aktualizacja Refresh Tokenów (usunięcie starego i dodanie nowego)
            refreshTokens.Remove(request.RefreshToken);
            refreshTokens[newRefreshToken] = request.Username;

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        private bool ValidateUserCredentials(string username, string password)
        {
            // Prosta weryfikacja - w rzeczywistości to powinno być na podstawie bazy danych
            return username == "Justyna" && password == "haslomaslo";
        }

        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }

    // Model danych dla Refresh Tokena
    public class RefreshTokenRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
