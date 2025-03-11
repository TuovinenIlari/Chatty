using Chatty.Data;
using Chatty.Data.Models;
using Chatty.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;


namespace Chatty.UnitTests
{
    public class MessageRepositoryTests
    {
        private ApplicationDbContext GetInMemoryApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private MessageRepository GetMessageRepository(ApplicationDbContext dbContext) 
        {
            return new MessageRepository(dbContext);
        }

        [Fact]
        public async Task AddMessage_ShouldFindMessage_AfterAdded()
        {
            //arrange
            var db = GetInMemoryApplicationDbContext();
            var messageRepository = GetMessageRepository(db);

            var testUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Test"
            };

            db.Users.Add(testUser);
            await db.SaveChangesAsync();

            var message = new Message
            {
                MessageId = new Guid(),
                Content = "Test message...",
                TimeStamp = DateTime.UtcNow,
                User = testUser
                
            };

            //act
            await messageRepository.AddMessage(message);
            var addedMessage = await messageRepository.GetMessageById(message.MessageId);

            //assert
            Assert.NotNull(addedMessage);
            Assert.Equal(message.MessageId, addedMessage.MessageId);
            Assert.Equal(message.Content, addedMessage.Content);
            Assert.Equal(message.TimeStamp, addedMessage.TimeStamp);
            Assert.Equal(testUser.Id, addedMessage.User.Id);
        }

        [Fact]
        public async Task GetRoomMessages_ShouldReturnGivenRoomMessages()
        {
            //arrange
            var db = GetInMemoryApplicationDbContext();
            var messageRepository = GetMessageRepository(db);

            var testUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Test"
            };

            var room1 = new ChatRoom {ChatRoomId = Guid.NewGuid(), Name = "Room 1"};
            var room2 = new ChatRoom {ChatRoomId = Guid.NewGuid(), Name = "Room 2"};

            var message1 = new Message { MessageId = Guid.NewGuid(), Content = "Hello from Room 1", ChatRoom = room1, User = testUser };
            var message2 = new Message { MessageId = Guid.NewGuid(), Content = "Another message in Room 1", ChatRoom = room1, User = testUser };
            var message3 = new Message { MessageId = Guid.NewGuid(), Content = "Message in Room 2", ChatRoom = room2, User = testUser };
            
            db.Users.Add(testUser);
            db.ChatRooms.AddRange(room1, room2);
            db.Messages.AddRange(message1, message2, message3);
            await db.SaveChangesAsync();

            

            // Act
            var messages = await messageRepository.GetRoomMessages(room1.ChatRoomId);

            // Assert
            Assert.NotNull(messages);
            Assert.Equal(2, messages.Count);  // Only 2 messages should be returned
            Assert.All(messages, m => Assert.Equal(room1.ChatRoomId, m.ChatRoom.ChatRoomId));
        }
    }
}
