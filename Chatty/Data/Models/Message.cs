namespace Chatty.Data.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
