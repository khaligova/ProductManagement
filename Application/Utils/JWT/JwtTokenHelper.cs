using Application.Utils.JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Utils.JWT
{
    public class JwtTokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Token CreateToken(string identifier, List<string> permissionClaims)
        {
            Token tokenResponse = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);
            DateTime expirationDate = DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiredDateAsMinute"]));

            Claim[] claims = {
                new (JwtRegisteredClaimNames.Sub, identifier),
                new("claims",JsonSerializer.Serialize(permissionClaims))
            };

            JwtSecurityToken jwtSecurityToken = new(
                claims: claims,
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                expires: expirationDate,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            tokenResponse.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return tokenResponse;
        }
        public bool ValidateToken(string rawToken)
        {
            if (rawToken is null)
                return false;

            string token = NormalizeRawToken(rawToken);

            JwtSecurityTokenHandler tokenHandler = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = securityKey,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"]
                }, out _);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string NormalizeRawToken(string rawToken)
        {
            string targetWord = "Bearer";
            int targetWordStartIndex = rawToken.IndexOf(targetWord);

            if (targetWordStartIndex < 0)
                return rawToken;

            string normalizedToken = rawToken.Remove(targetWordStartIndex, targetWord.Length).Trim();
            return normalizedToken;

        }
        public JwtSecurityToken DecodeToken(string token)
        {
            string normalizedToken = NormalizeRawToken(token);
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(normalizedToken);

            return jwtToken;
        }
        public string GetCurrentToken()
        {
            string rawToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            return rawToken;
        }
        public JwtSecurityToken GetCurrentDecodedToken()
        {
            string rawToken = GetCurrentToken();
            return DecodeToken(rawToken);
        }
        public string GetCurrentIdentifier()
        {
            return GetCurrentDecodedToken().Claims.First(claim => claim.Type == "sub").Value;
        }

        public string[] GetPermissionClaims()
        {
            string claimsAsString = GetCurrentDecodedToken().Claims.First(claim => claim.Type == "claims").Value;

            if (claimsAsString is null)
                return new string[0];

            return JsonSerializer.Deserialize<string[]>(claimsAsString);
        }
    }
}
