using Chatty.Data.Models;
using Chatty.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data.Services
{   //TODO: Validation
    public class ChatRoomService
    {
        private IMessageRepository _messageRepository;
        private IChatRoomRepository _chatRoomRepository;
        private IApplicationUserRepository _userRepository;

        public ChatRoomService(
            IMessageRepository messageRepository,
            IChatRoomRepository chatRoomRepository,
            IApplicationUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
        }
        public async Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId)
        {
            var chatrooms = await _chatRoomRepository.GetUserChatRooms(userId);
            return chatrooms;
        }

        public async Task<ChatRoom> CreateChatRoom(string name, string creatorId)
        {
            
            var chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
            // Add new chatroom to database
            var newChatroom =  await _chatRoomRepository.CreateChatRoom(chatRoom);
            // Add relationship
            await _userRepository.AddRoomRelationShip(chatRoom, creatorId);

            return chatRoom;   
        }
        public async Task<string> GetChatRoom(Guid chatRoomId)
        {

            var chatRoom = await _chatRoomRepository.GetChatRoomById(chatRoomId);

            if (chatRoom == null)
            {
                throw new KeyNotFoundException($"ChatRoom with id {chatRoomId} not found.");
            }

            return chatRoom.Name;
        }
    }
}
