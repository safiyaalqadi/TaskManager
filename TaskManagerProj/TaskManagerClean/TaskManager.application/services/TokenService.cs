using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace TaskManager.application.services
{
    public class TokenService :ITokenService
    {
        private readonly string _key;
        private readonly string _issuer;
       
        private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();

        public TokenService(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
         
        }



        public string CreateToken(string userName,string passWord, string userId, List<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                   new Claim(JwtRegisteredClaimNames.Sub, userName),

                  new Claim(ClaimTypes.NameIdentifier, userId), // ✅ userId
                  new Claim(ClaimTypes.Name, userName),

               // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               // new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("id",  userId),
                new Claim("role",  roles[0]),
                new Claim("userName", userName ),
                new Claim("passWord",  passWord )

            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string generateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            return principal;
        }


        
        public string RefreshAccessToken(string expiredToken, string refreshToken)
        {
            
            var principal = GetPrincipalFromExpiredToken(expiredToken);
            var userId =principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

           
            if (userId == null || !_refreshTokens.ContainsKey(userId) || _refreshTokens[userId] != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            
            var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var newToken = CreateToken(principal.Identity.Name,"password", userId, roles);

            
            var newRefreshToken = generateRefreshToken();
            _refreshTokens[userId] = newRefreshToken;

            return newToken;
        }

        public void SaveRefreshToken(string userId, string refreshToken)
        {
            _refreshTokens[userId] = refreshToken;
        }

        public void InvalidateRefreshToken(string userId)
        {
            _refreshTokens.Remove(userId); 
        }

    }
}
