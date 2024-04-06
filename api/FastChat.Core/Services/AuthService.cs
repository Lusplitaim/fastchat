using FastChat.Core.Models;
using FastChat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FastChat.Core.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUserEntity> m_UserManager;
        private SignInManager<AppUserEntity> m_SignInManager;

        public AuthService(UserManager<AppUserEntity> userManager, SignInManager<AppUserEntity> signInManager)
        {
            m_UserManager = userManager;
            m_SignInManager = signInManager;
        }

        public async Task SignUpAsync(CreateUser model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            AppUserEntity appUser = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await m_UserManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create new user");
            }
        }

        public async Task SignInAsync(SignUser model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var existingUser = await m_UserManager.FindByEmailAsync(model.Email);
            if (existingUser is null)
            {
                throw new Exception("User doesn't exist");
            }

            var result = await m_SignInManager.CheckPasswordSignInAsync(existingUser, model.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid password");
            }
        }

        public Task SignOutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
