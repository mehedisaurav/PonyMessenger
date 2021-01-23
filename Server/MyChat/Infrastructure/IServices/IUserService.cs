using MyChat.Domain;
using MyChat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Infrastructure.IServices
{
    public interface IUserService : IChatRepository<User>
    {
        Task<ICollection<UserViewModel>> UserList();
        Task CreateUser(User user);
        Task<bool> IsExistUser(string email);
        Task<UserViewModel> GetUser(string email);
    }
}
