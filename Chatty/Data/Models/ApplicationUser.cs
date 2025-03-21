using Microsoft.AspNetCore.Identity;

namespace Chatty.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Navigation properties
        public ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }

}
