using FashionShop.Application.Auth;
using FashionShop.Application.Common;
using FashionShop.Contracts.Auth;
using FashionShop.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FashionShop.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthService(IRepository<User> userRepo, IUnitOfWork uow, IConfiguration config)
        {
            _userRepo = userRepo;
            _uow = uow;
            _config = config;
        }

        public async Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request, CancellationToken ct = default)
        {
            // check exists by username or email
            var exists = (await _userRepo.Query().FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.Email, ct)) != null;
            if (exists)
                return new UserRegisterResponse { Succeeded = false, Message = "UserName or Email already exists" };

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = UserStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user, ct);
            await _uow.SaveChangesAsync(ct);

            return new UserRegisterResponse { Succeeded = true };
        }

        public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request, CancellationToken ct = default)
        {
            // find user by username or email
            var user = await _userRepo.Query().FirstOrDefaultAsync(u => u.UserName == request.UserNameOrEmail || u.Email == request.UserNameOrEmail, ct);
            if (user == null)
                return new UserLoginResponse { Succeeded = false, Message = "Invalid credentials" };

            var ok = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!ok) return new UserLoginResponse { Succeeded = false, Message = "Invalid credentials" };

            // create JWT
            var jwtCfg = _config.GetSection("Jwt");
            // Use indexer access to avoid relying on IConfiguration Binder extension methods
            var key = jwtCfg["Key"];
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("JWT key missing");

            var issuer = jwtCfg["Issuer"];
            var audience = jwtCfg["Audience"];

            // Parse expire minutes safely, provide a sensible default if missing or invalid
            var expireMinutes = 60; // default
            var expireStr = jwtCfg["ExpireMinutes"];
            if (!string.IsNullOrWhiteSpace(expireStr) && int.TryParse(expireStr, out var parsed))
            {
                expireMinutes = parsed;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
                // add role claims later
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserLoginResponse { Succeeded = true, Token = jwt, ExpiresAt = token.ValidTo };
        }
    }
}
