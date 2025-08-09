using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BackEnd.DTO.User;
using BackEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Services
{
    public class JwtProvider
    {
        private readonly JwtOption _options;

        public JwtProvider(IOptions<JwtOption> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(UserDto user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            Claim[] claims =
            [
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.Name),
                new Claim("UserEmail", user.Email)
            ];

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpirationHours),
                signingCredentials: signingCredentials
            );

            var tokenValues = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValues;
        }
    }
}
