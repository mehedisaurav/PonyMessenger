using Microsoft.EntityFrameworkCore;
using MyChat.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Infrastructure
{
    public abstract class ChatRepositoryBase<T> : IChatRepository<T> where T : class
    {
        private readonly MyChatDbContext _context;

        public ChatRepositoryBase(MyChatDbContext context)
        {
            _context = context;
        }
        //public async Task Create(T entity)
        //{
        //    this._context.Set<T>().Add(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public void Delete(T entity)
        //{
        //    this._context.Set<T>().Remove(entity);
        //}

        //public IQueryable<T> FindAll()
        //{
        //    return this._context.Set<T>().AsNoTracking();
        //}


    }
}
