using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChat.Infrastructure.IServices;
using MyChat.ViewModel;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<ActionResult<IQueryable<MessageViewModel>>> GetMessages(int senderId, int receiverId)
        {
            var messages = await _service.GetMesssages(senderId, receiverId);
            return Ok(messages);
        }

        [HttpPost]
        [Route("SaveMessage")]
        public async Task SaveMessage(MessageViewModel msg)
        {
            await _service.CreateMessage(msg);
        }

        [HttpPost]
        [Route("DeleteMessage")]
        public async Task DeleteMessage(MessageViewModel msg)
        {
            await _service.DeleteMessage(msg);
        }
    }
}