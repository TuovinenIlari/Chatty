using Chatty.Data.Models;

namespace Chatty.Data.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        Task AddRoomRelationShip(ChatRoom chatroom);
        Task AddMessageRelationShip(Message message);
    }
}
