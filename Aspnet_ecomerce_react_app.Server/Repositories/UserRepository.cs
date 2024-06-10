using Aspnet_ecomerce_react_app.Server.Data;
using Aspnet_ecomerce_react_app.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspnet_ecomerce_react_app.Server.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    return user;
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting user by ID: {ex.Message}");
                return null;
            }
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if(user != null)
                {
                    return user;
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting user by username: {ex.Message}");
                return null;
            }
        }

        public async Task<List<string>> GetRolesForUserAsync(int userId)
        {
            var user = await _context.Users
            .Include(u => u.Roles) // Ensure Roles collection is loaded
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Roles == null)
            {
                return new List<string>(); // Return empty list if user or roles are null
            }

            var userRoles = user.Roles.Select(r => r.Name).ToList();

            return userRoles;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting all users: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<List<User>> GetPagedUsersAsync(int pageIndex, int pageSize, string sortBy)
        {
            try
            {
                IQueryable<User> query = _context.Users;

                switch (sortBy.ToLower())
                {
                    case "username":
                        query = query.OrderBy(u => u.Username);
                        break;
                    case "email":
                        query = query.OrderBy(u => u.Email);
                        break;
                    case "id":
                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }

                return await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting paged users: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<User?> AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding user: {ex.Message}");
                return null;
            }
        }
        public async Task<User?> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating user: {ex.Message}");
                return null;
            }
        }

        public async Task<User?> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return null;
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user: {ex.Message}");
                return null;
            }
        }
    }
}
