using Microsoft.EntityFrameworkCore;
using MyChat.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Configuration
{
    public class MyChatDbContext : DbContext
    {
        public MyChatDbContext(DbContextOptions<MyChatDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageMap());
        }

    }
}
