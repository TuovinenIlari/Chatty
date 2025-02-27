namespace Chatty.Data.Models
{
    public class ChatRoom
    {
        public Guid ChatRoomId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt{ get; set;}
        public string? PrivateChatKey { get; set; }

        // Navigation properties
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
