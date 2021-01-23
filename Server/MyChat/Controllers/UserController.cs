using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyChat.Domain;
using MyChat.Infrastructure.IServices;
using MyChat.ViewModel;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<UserViewModel>>> Get()
        {
            var users = await  _service.UserList();

            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<bool>> CreateUser(UserViewModel user)
        {
            var model = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            if (!await _service.IsExistUser(user.Email))
            {
                await _service.CreateUser(model);
                return Ok(true);
            }

            return Ok(false);

        }

        [HttpGet]
        [Route("Login")]
        public async Task<UserViewModel> Login(string email)
        {
            return await _service.GetUser(email);
        }
    }
}