using Chatty.Components.Pages;
using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public MessageRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task AddMessage(Message message)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Add(message);
            dbContext.SaveChanges();

            throw new NotImplementedException();
        }

        public Task DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetMessageById(string id)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var message = await dbContext.Messages.FindAsync(id);

            if (message == null) 
            {
                throw new KeyNotFoundException($"Message with id {id} not found.");
            }

            return message;
        }

        public async Task<List<Message>> GetRoomMessages(Guid roomId)
        {

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var messages = await dbContext.Messages
                                          .Where(m => m.ChatRoom.ChatRoomId == roomId)
                                          .ToListAsync();

            return messages;
        }

        public Task<List<Message>> GetUserMessages(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
