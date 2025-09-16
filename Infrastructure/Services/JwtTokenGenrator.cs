using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;


        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public TokenResult GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddMinutes(_settings.DurationInMinutes);

            var token = new JwtSecurityToken
                (
                _settings.Issuer,
                _settings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_settings.DurationInMinutes),
                signingCredentials: creds
                );

            return new TokenResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiry
            };

        }
    }
}
