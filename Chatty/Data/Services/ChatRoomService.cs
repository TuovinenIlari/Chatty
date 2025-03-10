using Chatty.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Services
{   //TODO: Validation
    public class ChatRoomService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ChatRoomService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

          return  await dbContext.ChatRooms
                    .Where(c => c.ApplicationUsers.Select(u => u.Id).Contains(userId))
                    .ToListAsync();
        }

        public async Task<ChatRoom> CreateChatRoom(string name, string creatorId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
            // Add new chatroom to database
             dbContext.Add(chatRoom);

            // Find user who created chatroom from database
            var user = dbContext.Users.First(u => u.Id == creatorId);
            // Add relationship
            user.ChatRooms.Add(chatRoom);
            
            await dbContext.SaveChangesAsync();

            return chatRoom;   
        }
        public async Task<string> GetChatRoom(Guid chatRoomId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var chatRoom = await dbContext.ChatRooms.FindAsync(chatRoomId);

            if (chatRoom == null)
            {
                throw new KeyNotFoundException($"ChatRoom with id {chatRoomId} not found.");
            }

            return chatRoom.Name;
        }
    }
}
