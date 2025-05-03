using InventoryClassLibrary.DTO;
using InventoryClassLibrary.Models;
using InventoryManagmentSystem.Features.Auth.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagmentSystem.Features.Auth.Command.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResultStatus>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResultStatus> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerData = request.RegisterData;


            ApplicationUser newUser = new()
            {
                Email = registerData.Email,
                UserName = registerData.UserName
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerData.Password);

            if (!identityResult.Succeeded)
            {
                string errors = "";
                foreach (var error in identityResult.Errors)
                {
                    errors += error.Description;
                }

                return new ResultStatus
                {
                    Status = false,
                    Message = errors
                };
            }

            const string userRole = "User";
            var roleExists = await _roleManager.RoleExistsAsync(userRole);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(userRole));
            }

            await _userManager.AddToRoleAsync(newUser, userRole);

            return new ResultStatus
            {
                Status = true,
                Message = "User created successfully"
            };
        }
    }
}
