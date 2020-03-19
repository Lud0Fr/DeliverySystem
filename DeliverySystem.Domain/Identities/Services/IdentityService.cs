using DeliverySystem.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliverySystem.Domain.Identities.Services
{
    #region Interface

    public interface IIdentityService
    {
        string GenerateJWT(Identity identity);
    }

    #endregion

    public class IdentityService : IIdentityService
    {
        private readonly JwtConfiguration _jWTConfiguration;
        private readonly IConfiguration _configuration;

        public IdentityService(
            JwtConfiguration jWTConfiguration,
            IConfiguration configuration)
        {
            _jWTConfiguration = jWTConfiguration;
            _configuration = configuration;
        }

        public string GenerateJWT(Identity identity)
        {
            var userDetails = new { identity.Id, identity.Role };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                new Claim(IdentityClaims.UserDetailsKey, JsonConvert.SerializeObject(userDetails, Formatting.None)),
                new Claim(ClaimTypes.Role, identity.Role.ToString())
            };

            var notBeforeDateTime = string.IsNullOrEmpty(_jWTConfiguration.TokenExpiresNotBefore)
                 ? DateTime.UtcNow
                 : DateTime.ParseExact(_jWTConfiguration.TokenExpiresNotBefore, "yyMMddHHmm", CultureInfo.InvariantCulture);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                 issuer: _jWTConfiguration.Issuer,
                 audience: _jWTConfiguration.Audience,
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(_jWTConfiguration.TokenExpiresInDays),
                 notBefore: notBeforeDateTime,
                 signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
