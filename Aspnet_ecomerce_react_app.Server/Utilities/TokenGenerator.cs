using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aspnet_ecomerce_react_app.Server.Utilities
{
    public class TokenGenerator
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly byte[] _key;

        public TokenGenerator(IConfiguration configuration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            var key = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");
            }
            _key = Encoding.ASCII.GetBytes(key);
        }

        public string GenerateToken(string userId, string username, List<string> roles)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, string.Join(",", roles))
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }
    }   
}
