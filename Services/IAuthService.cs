using API_Authentication_BasicImplementation.Entities;
using API_Authentication_BasicImplementation.Models;

namespace API_Authentication_BasicImplementation.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<TokenResponseDTO?> LoginAsync(UserDTO request);

        Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenRequestDTO request);
    }
}
