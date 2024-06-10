using Aspnet_ecomerce_react_app.Server.Models;
using Aspnet_ecomerce_react_app.Server.Repositories;

namespace Aspnet_ecomerce_react_app.Server.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(userId);
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
                return await _userRepository.GetUserByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting user by username: {ex.Message}");
                return null;
            }
        }

        public async Task<List<string>> GetRolesForUserAsync(int userId)
        {
            return await _userRepository.GetRolesForUserAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<List<User>> GetPagedUsersAsync(int pageIndex, int pageSize, string sortBy)
        {
            return await _userRepository.GetPagedUsersAsync(pageIndex, pageSize, sortBy);
        }

        public async Task<User?> AddUserAsync(User user)
        {
            try
            {
                return await _userRepository.AddUserAsync(user);
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
                return await _userRepository.UpdateUserAsync(user);
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
                return await _userRepository.DeleteUserAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user: {ex.Message}");
                return null;
            }
        }
    }
}
