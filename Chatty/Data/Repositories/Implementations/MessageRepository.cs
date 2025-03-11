using Chatty.Components.Pages;
using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public MessageRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task AddMessage(Message message)
        {
            _dbcontext.Messages.Add(message);
            await _dbcontext.SaveChangesAsync();
        }

        public Task DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetMessageById(Guid id)
        {
            var message = await _dbcontext.Messages.FindAsync(id);

            if (message == null) 
            {
                throw new KeyNotFoundException($"Message with id {id} not found.");
            }

            return message;
        }

        public async Task<List<Message>> GetRoomMessages(Guid roomId)
        {

            var messages = await _dbcontext.Messages
                                 .Where(m => m.ChatRoom.ChatRoomId == roomId)
                                 .ToListAsync();

            return messages;
        }

        public Task<List<Message>> GetUserMessages(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
