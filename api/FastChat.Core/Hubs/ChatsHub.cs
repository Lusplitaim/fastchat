using FastChat.Core.Models;
using FastChat.Core.Repositories;
using FastChat.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FastChat.Core.Hubs
{
    [Authorize]
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

        public async Task SendMessage(long chatId, string message)
        {
            var messageEntity = await m_UnitOfWork.ChatMessageRepository.SaveAsync(chatId, GetCurrentUserId(), message);
            m_UnitOfWork.SaveChanges();
            await Clients.Group($"chat_{chatId}").SendAsync("ReceiveMessage", messageEntity);
        }

        public async Task SubscribeToChats(List<long> chatIds)
        {
            foreach (var chatId in chatIds)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
            }
        }

        public async Task SubscribeToChat(long chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        }

        public async Task InitDialog(int recipientId, string message)
        {
            try
            {
                var senderId = GetCurrentUserId();
                var existing = m_UnitOfWork.ChatRepository.GetDialog(senderId, recipientId);
                if (existing is not null) return;

                ChatEntity chat = await m_UnitOfWork.ChatRepository.CreateDialogAsync(senderId, recipientId);
                m_UnitOfWork.SaveChanges();

                var messageEntity = await m_UnitOfWork.ChatMessageRepository.SaveAsync(chat.Id, senderId, message);
                m_UnitOfWork.SaveChanges();

                List<AppUserEntity> users = m_UnitOfWork.UserRepository.Get([senderId, recipientId]);
                var recipient = users.Single(u => u.Id == recipientId);
                Chat chatForSender = new()
                {
                    Id = chat.Id,
                    Name = string.Join(' ', recipient.FirstName, recipient.LastName),
                    Type = chat.Type,
                    Recipient = ChatUser.CreateFrom(recipient),
                    Messages = [ChatMessage.CreateFrom(messageEntity)],
                };
                await Clients.Caller.SendAsync("ReceiveNewChat", chatForSender);

                var sender = users.Single(u => u.Id == senderId);
                Chat chatForRecipient = new()
                {
                    Id = chat.Id,
                    Name = string.Join(' ', sender.FirstName, sender.LastName),
                    Type = chat.Type,
                    Recipient = ChatUser.CreateFrom(sender),
                    Messages = [ChatMessage.CreateFrom(messageEntity)],
                };
                await Clients.User(recipientId.ToString()).SendAsync("ReceiveNewChat", chatForRecipient);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create a dialog", ex);
            }
        }

        private int GetCurrentUserId()
        {
            return Convert.ToInt32(Context.UserIdentifier);
        }
    }
}
