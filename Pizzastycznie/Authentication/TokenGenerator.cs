using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pizzastycznie.Authentication.DTO;
using Pizzastycznie.Database.DTO;

namespace Pizzastycznie.Authentication
{
    public static class TokenGenerator
    {
        public static string GenerateToken(SelectUserObject user, DateTimeOffset expirationTime)
        {
            var userRole = user.IsAdmin ? UserRole.Admin : UserRole.User;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, userRole),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECURITY_KEY"))),
                    SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}