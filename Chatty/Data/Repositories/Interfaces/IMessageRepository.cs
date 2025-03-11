using Chatty.Data.Models;

namespace Chatty.Data.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        public Task AddMessage(Message message);
        public Task UpdateMessage(Message message);
        public Task DeleteMessage(Message message);
        public Task<Message> GetMessageById(Guid id);
        public Task<List<Message>> GetUserMessages(Guid userId);
        public Task<List<Message>> GetRoomMessages(Guid roomId);
    }
}
