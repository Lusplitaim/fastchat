using FastChat.Core.Models;
using FastChat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FastChat.Core.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUserEntity> m_UserManager;

        public AuthService(UserManager<AppUserEntity> userManager)
        {
            m_UserManager = userManager;
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

        public Task SignInAsync()
        {
            return Task.CompletedTask;
        }

        public Task SignOutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
