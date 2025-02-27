using Chatty.Data.Models;

namespace Chatty.Data.Services
{   //TODO: Validation
    public class ChatRoomService
    {
        private readonly ApplicationDbContext _dbcontext;

        public ChatRoomService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<ChatRoom> CreateChatRoom(string name, string creatorId)
        {
            var chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
            // Add new chatroom to database
             _dbcontext.Add(chatRoom);

            // Find user who created chatroom from database
            var user = _dbcontext.Users.First(u => u.Id == creatorId);
            // Add relationship
            user.ChatRooms.Add(chatRoom);
            
            await _dbcontext.SaveChangesAsync();

            return chatRoom;
            
        }
    }
}
