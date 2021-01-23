using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MyChat.Configuration;
using MyChat.Domain;
using MyChat.Infrastructure.Hubs;
using MyChat.Infrastructure.IServices;
using MyChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Infrastructure.Services
{
    public class MessageService : ChatRepositoryBase<Message>, IMessageService
    {
        public MyChatDbContext _chatDbContext;
        private readonly IHubContext<SignalHub> _hubContext;

        public MessageService(MyChatDbContext chatDbContext, IHubContext<SignalHub> hubContext) : base(chatDbContext)
        {
            _chatDbContext = chatDbContext;
            _hubContext = hubContext;
        }

        public async Task CreateMessage(MessageViewModel msg)
        {
            var message = new Message()
            {
                IsDeletedBySender = false,
                IsDeletedByReceiver = false,
                ReceiverId = msg.ReceiverId,
                SenderId = msg.SenderId,
                Text = msg.Text,
                TextTime = DateTime.Now
            };

            try
            {
                _chatDbContext.Messages.Add(message);
                await _chatDbContext.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("MessageSave", msg.ReceiverId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteMessage(MessageViewModel msg)
        {
            var message = await _chatDbContext.Messages.FindAsync(msg.Id);
            message.IsDeletedBySender = msg.IsDeletedBySender;
            message.IsDeletedByReceiver = msg.IsDeletedByReceiver;
            message.IsDeleteAll = msg.IsDeletedAll;
            await _chatDbContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("MessageRemove", msg.ReceiverId);
        }

        public async Task<List<MessageViewModel>> GetMesssages(int senderId, int receiverId)
        {

            var msgList = await _chatDbContext.Messages
                                .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || 
                                            (x.SenderId == receiverId && x.ReceiverId == senderId))
                                .Select(x => new MessageViewModel
                                {
                                    Id = x.Id,
                                    Text = x.Text,
                                    TextTime = x.TextTime,
                                    ReceiverId = x.ReceiverId,
                                    SenderId = x.SenderId,
                                    IsDeletedBySender = x.IsDeletedBySender,
                                    IsDeletedByReceiver = x.IsDeletedByReceiver,
                                    IsDeletedAll = x.IsDeleteAll
                                }).ToListAsync();

            return msgList;

        }
    }
}
