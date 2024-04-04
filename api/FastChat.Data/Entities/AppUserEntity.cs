using Microsoft.AspNetCore.Identity;

namespace FastChat.Data.Entities
{
    public class AppUserEntity : IdentityUser<int>
    {
        public override string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
