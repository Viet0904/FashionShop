

namespace FashionShop.Contracts.Auth
{
    public class UserLoginRequest
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
