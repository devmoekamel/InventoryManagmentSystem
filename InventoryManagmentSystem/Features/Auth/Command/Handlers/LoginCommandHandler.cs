using InventoryManagmentSystem.Core.DTO.Auth;
using InventoryManagmentSystem.Core.Models;
using InventoryManagmentSystem.Features.Auth.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagmentSystem.Features.Auth.Command.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginData = request.LoginData;

            var user = await _userManager.FindByNameAsync(loginData.UserName);
            if (user == null)
                return new LoginResult
                {
                    Status = false,
                    Message = "Username or password is wrrgong"
                };

            var result = await _userManager.CheckPasswordAsync(user, loginData.Password);
            if (!result)
                return new LoginResult
                {
                    Status = false,
                    Message = "Username or password is wrrgong"
                };

            string jti = Guid.NewGuid().ToString();
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(2);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Iss"],
                audience: _config["JWT:Aud"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new LoginResult
            {
                Status = true,
                Message = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = expiry
            };
        }
    }
}
