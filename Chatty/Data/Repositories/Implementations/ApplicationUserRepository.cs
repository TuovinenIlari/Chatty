using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Repositories.Implementations
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ApplicationUserRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public Task AddMessageRelationShip(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task AddRoomRelationShip(ChatRoom chatroom, string userId)
        {
            var user = await _dbcontext.Users
                .Include(u => u.ChatRooms) // Ensure navigation property is loaded
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            user.ChatRooms.Add(chatroom); // Add the chatroom relationship
            await _dbcontext.SaveChangesAsync(); // Save to DB
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _dbcontext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found...");
            }
            return user;
        }
    }
}
