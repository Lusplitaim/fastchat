using FastChat.Data.Models;

namespace FastChat.Core.Models
{
    public class SearchChat
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }

    }
}
