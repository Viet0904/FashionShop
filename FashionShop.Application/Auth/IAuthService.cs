using FashionShop.Contracts.Auth;

namespace FashionShop.Application.Auth
{
    public interface IAuthService
    {
        Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request, CancellationToken ct = default);
        Task<UserLoginResponse> LoginAsync(UserLoginRequest request, CancellationToken ct = default);
    }
}
