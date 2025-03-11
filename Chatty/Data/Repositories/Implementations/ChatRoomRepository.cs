using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Repositories.Implementations
{


    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ChatRoomRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async Task<ChatRoom> CreateChatRoom(ChatRoom chatroom)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Add(chatroom);

            await dbContext.SaveChangesAsync();

            return chatroom;
        }

        public async Task DeleteChatRoom(string chatRoomId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var chatRoom = await dbContext.ChatRooms.FindAsync(chatRoomId);

            if (chatRoom == null)
            {
                throw new KeyNotFoundException($"ChatRoom with id {chatRoomId} not found.");
            }

            dbContext.Remove(chatRoom);

            await dbContext.SaveChangesAsync();

        }

        public async Task<ChatRoom> GetChatRoomById(string chatRoomId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var chatRoom = await dbContext.ChatRooms.FindAsync(chatRoomId);

            if (chatRoom == null)
            {
                throw new KeyNotFoundException($"ChatRoom with id {chatRoomId} not found.");
            } 

            return chatRoom;
        }

        public Task<ChatRoom> GetChatRoomByName(string roomName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatRoom>> GetUserChatRooms(string userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            var chatRooms = await dbContext.ChatRooms
                            .Where(c => c.ApplicationUsers.Select(u => u.Id).Contains(userId))
                            .ToListAsync();

            if (chatRooms == null)
            {
                throw new KeyNotFoundException($"No rooms found...");
            }

            return chatRooms;
        }
    }
}
