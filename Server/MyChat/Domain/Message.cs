using System;

namespace MyChat.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsDeletedBySender { get; set; }
        public bool IsDeletedByReceiver { get; set; } 
        public bool IsDeleteAll { get; set; }  
        public string Text { get; set; }
        public DateTime TextTime { get; set; }  
        public virtual User Receiver { get; set; }

    }
}
