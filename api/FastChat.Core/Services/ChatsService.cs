using FastChat.Core.Models;
using FastChat.Core.Repositories;
using FastChat.Data.Entities;
using FastChat.Data.Models;

namespace FastChat.Core.Services
{
    public class ChatsService : IChatsService
    {
        private IUnitOfWork m_UnitOfWork;
        private IUserService m_UserService;
        public ChatsService(IUnitOfWork uow, IUserService userService)
        {
            m_UnitOfWork = uow;
            m_UserService = userService;
        }

        public List<Chat> GetChats(int userId)
        {
            var user = m_UnitOfWork.UserRepository.Get(userId);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            var chats = m_UnitOfWork.ChatRepository.GetUserChats(user.Id);
            var channels = m_UnitOfWork.ChannelRepository.Get(chats.Select(c => c.Id).ToList());
            var chatMembers = m_UnitOfWork.ChatMemberRepository.GetDialogRecipients(user.Id);

            List<Chat> result = [];
            foreach (var chat in chats)
            {
                var userChat = new Chat() { Id = chat.Id, Type = chat.Type, Messages = [..chat.Messages.Select(ChatMessage.CreateFrom)] };
                switch (chat.Type)
                {
                    case ChatType.Personal:
                    case ChatType.Dialog:
                        var recipient = chatMembers.Single(cm => cm.ChatId == chat.Id).User;
                        ChatUser chatUser = CreateChatUserFrom(recipient);
                        userChat.Name = chatUser.Name;
                        userChat.Recipient = chatUser;
                        break;
                    case ChatType.Channel:
                    case ChatType.Group:
                        var channel = channels.Single(c => c.ChatId == chat.Id);
                        ChatChannel chatChannel = new()
                        {
                            Id = channel.Id,
                            Name = channel.Name,
                        };
                        userChat.Name = channel.Name;
                        userChat.Channel = chatChannel;
                        break;
                }
                result.Add(userChat);
            }

            return result;
        }

        public List<Chat> FindChats(string keyword)
        {
            var users = m_UnitOfWork.UserRepository.Find(keyword);
            var channels = m_UnitOfWork.ChannelRepository.Find(keyword);

            List<Chat> result = [];
            foreach (var user in users)
            {
                ChatUser chatUser = CreateChatUserFrom(user);
                result.Add(new() { Id = null, Name = string.Join(' ', user.FirstName, user.LastName), Type = ChatType.Dialog, Recipient = chatUser });
            }

            foreach (var channel in channels)
            {
                ChatChannel chatChannel = new()
                {
                    Id = channel.Id,
                    Name = channel.Name,
                };
                result.Add(new() { Id = null, Name = channel.Name, Type = channel.Chat.Type, Channel = chatChannel });
            }

            return result;
        }

        public Chat GetChat(long chatId)
        {
            var chatEntity = m_UnitOfWork.ChatRepository.Get(chatId);
            if (chatEntity == null)
            {
                throw new Exception("Chat does not exist");
            }

            Chat chat = new()
            {
                Id = chatEntity.Id,
                Type = chatEntity.Type,
            };

            if (chatEntity.Type == ChatType.Dialog)
            {
                var chatMemberEntity = m_UnitOfWork.ChatMemberRepository.GetDialogRecipient(chatId, m_UserService.GetAuthUserId());
                if (chatMemberEntity is null)
                {
                    throw new Exception("Chat does not exist");
                }

                ChatUser chatUser = CreateChatUserFrom(chatMemberEntity.User);
                chat.Name = chatUser.Name;
                chat.Recipient = chatUser;
            }

            return chat;
        }

        public Chat GetChatWithUser(int userId)
        {
            var recipientId = userId;
            var recipient = m_UnitOfWork.UserRepository.Get(recipientId);
            if (recipient is null)
            {
                throw new Exception("User does not exist");
            }

            var currentUserId = m_UserService.GetAuthUserId();
            var chatEntity = m_UnitOfWork.ChatRepository.GetDialog(currentUserId, recipientId);
            Chat chat = new()
            {
                Id = chatEntity?.Id,
                Name = string.Join(' ', recipient.FirstName, recipient.LastName),
                Type = chatEntity != null
                    ? chatEntity.Type
                    : (currentUserId == recipientId ? ChatType.Personal : ChatType.Dialog),
                Recipient = CreateChatUserFrom(recipient),
            };

            return chat;
        }

        private ChatUser CreateChatUserFrom(AppUserEntity userEntity)
        {
            return new()
            {
                Id = userEntity.Id,
                Name = string.Join(' ', userEntity.FirstName, userEntity.LastName),
                UserName = userEntity.UserName,
            };
        }
    }
}
