using Microsoft.EntityFrameworkCore;
using MyChat.Configuration;
using MyChat.Domain;
using MyChat.Infrastructure.IServices;
using MyChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Infrastructure.Services
{
    public class UserService : ChatRepositoryBase<User>, IUserService
    {

        public MyChatDbContext _chatDbContext;
        public UserService(MyChatDbContext chatDbContext) : base(chatDbContext){
            _chatDbContext = chatDbContext;
        }

        public async Task<ICollection<UserViewModel>> UserList()
        {
            var users = await _chatDbContext.Users
                        .Select(s => new UserViewModel
                            {
                                Id = s.Id,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Email = s.Email
                            }).ToListAsync();

            return users;
        }

        public async Task CreateUser(User user)
        {
            try
            {
                _chatDbContext.Users.Add(user);
                await _chatDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UserViewModel> GetUser(string email)
        {
            var user = await _chatDbContext.Users
                        .Where(x => x.Email == email.Trim())
                        .Select(x=> new UserViewModel
                        {
                            Id = x.Id,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> IsExistUser(string email)
        {
            return await  _chatDbContext.Users.AnyAsync(x => x.Email == email.Trim());
        }

        
    }
}
