using Chatty.Data.Models;

namespace Chatty.Data.Repositories.Interfaces
{
    public interface IChatRoomRepository
    {
        Task<List<ChatRoom>> GetUserChatRooms(string userId);
        Task<ChatRoom> CreateChatRoom(ChatRoom chatRoom);
        Task<ChatRoom> GetChatRoomById(string chatRoomId);
        Task<ChatRoom> GetChatRoomByName(string roomName);
        Task DeleteChatRoom(string chatRoomId);
    }
}
