using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pizzastycznie.Authentication.DTO;

namespace Pizzastycznie.Authentication
{
    public static class TokenGenerator
    {
        public static UserAuthenticationResponseObject GenerateToken(string email)
        {
            var expirationTime = new DateTimeOffset(DateTime.Now).AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECURITY_KEY"))),
                    SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            return new UserAuthenticationResponseObject
            {
                Email = email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expirationTime.ToString()
            };
        }
    }
}