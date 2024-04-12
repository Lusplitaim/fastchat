using FastChat.Core.Repositories;
using FastChat.Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FastChat.Core.Hubs
{
    public class ChatsHub : Hub
    {
        private IUnitOfWork m_UnitOfWork;
        public ChatsHub(IUnitOfWork unitOfWork)
        {
            m_UnitOfWork = unitOfWork;
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        }

        public async Task SendMessage(int chatId, string message)
        {
            ChatMessageEntity messageEntity = new()
            {
                Id = new Random(1).Next(),
                Content = message,
            };
            await Clients.Group($"chat_{chatId}").SendAsync("ReceiveMessage", messageEntity);
        }

        public async Task InitDialog(int recipientId, string message)
        {
            var senderId = Convert.ToInt32(Context.UserIdentifier);
            var existing = m_UnitOfWork.ChatRepository.GetDialog(senderId, recipientId);
            if (existing is not null) return;

            ChatEntity chat = await m_UnitOfWork.ChatRepository.CreateDialogAsync(senderId, recipientId);
            var groupName = $"chat_{chat.Id}";
            //await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            //await Clients.User(recipientId.ToString()).SendAsync("ReceiveNewChat", chat.Id);
            //var messageEntity = await m_UnitOfWork.ChatMessageRepository.SaveAsync(chat.Id, senderId, message);
            //await Clients.Group(groupName).SendAsync("Send", messageEntity);
        }
    }
}
