using MyChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Domain
{
    public class User
    {
        public User()
        {
            //SenderMessages = new HashSet<Message>();
            ReceiverMessages = new HashSet<Message>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLogged { get; set; } 
        //public virtual ICollection<Message> SenderMessages { get; set; }
        public virtual ICollection<Message> ReceiverMessages { get; set; }
    }
}
