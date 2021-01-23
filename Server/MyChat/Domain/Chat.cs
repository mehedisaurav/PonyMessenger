using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
