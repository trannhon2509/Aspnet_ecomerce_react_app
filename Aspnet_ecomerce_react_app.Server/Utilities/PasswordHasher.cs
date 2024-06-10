using System.Text;
using System.Security.Cryptography;

namespace Aspnet_ecomerce_react_app.Server.Utilities
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public bool VerifyPassword(string hashedPassword, string password)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
