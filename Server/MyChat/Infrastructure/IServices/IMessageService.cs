using MyChat.Domain;
using MyChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Infrastructure.IServices
{
    public interface IMessageService : IChatRepository<Message>
    {
        Task CreateMessage(MessageViewModel msg);
        Task<List<MessageViewModel>> GetMesssages(int  senderId, int receiverId);
        Task DeleteMessage(MessageViewModel msg);
    }
}
