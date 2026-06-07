using BookStore.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(string userId, string roleName); // لـ CRUD الخاص بالـ Roles
    }
}