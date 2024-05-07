using FastChat.Data.Entities;

namespace FastChat.Core.Models
{
    public class ChatUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public static ChatUser CreateFrom(AppUserEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = string.Join(' ', entity.FirstName, entity.LastName),
                UserName = entity.UserName,
            };
        }
    }
}
