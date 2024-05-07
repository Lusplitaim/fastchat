using FastChat.Data.Entities;

namespace FastChat.Core.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }

        public static AppUser FromEntity(AppUserEntity appUser)
        {
            return new()
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email!,
            };
        }
    }
}
