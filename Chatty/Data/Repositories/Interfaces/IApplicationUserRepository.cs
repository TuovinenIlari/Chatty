using Chatty.Data.Models;

namespace Chatty.Data.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserById(string id);
        Task AddRoomRelationShip(ChatRoom chatroom, string userId);
        Task AddMessageRelationShip(Message message);
    }
}
