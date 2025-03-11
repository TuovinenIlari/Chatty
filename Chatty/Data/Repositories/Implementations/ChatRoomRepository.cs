using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Repositories.Implementations
{


    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ChatRoomRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<ChatRoom> CreateChatRoom(ChatRoom chatroom)
        {
            _dbcontext.Add(chatroom);

            await _dbcontext.SaveChangesAsync();

            return chatroom;
        }

        public async Task DeleteChatRoom(string chatRoomId)
        {
            var chatRoom = await _dbcontext.ChatRooms.FindAsync(chatRoomId);

            if (chatRoom == null)
            {
                throw new KeyNotFoundException($"ChatRoom with id {chatRoomId} not found.");
            }

            _dbcontext.Remove(chatRoom);

            await _dbcontext.SaveChangesAsync();

        }

        public async Task<ChatRoom> GetChatRoomById(Guid chatRoomId)
        {
            var chatRoom = await _dbcontext.ChatRooms.FindAsync(chatRoomId);

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
            var chatRooms = await _dbcontext.ChatRooms
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
