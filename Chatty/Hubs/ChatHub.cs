using Chatty.Data.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace Chatty.Hubs
{

    public class ChatHub : Hub
    {
        private ChatRoomService _roomService;

        public ChatHub(ChatRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task SendMessage(string user, string message, string roomId)
        {
            Console.WriteLine($"Invoke SendMessage {Context.ConnectionId}");
            await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message);
        }
        public async Task JoinRoom(Guid roomId)
        {
            var roomName = await _roomService.GetChatRoom(roomId);

            // Cast Guid roomId to string for group funcs
            var strRoomId = roomId.ToString();

            if (string.IsNullOrEmpty(roomName))
            {
                // Room does not exist, send an error message back to the caller
                await Clients.Caller.SendAsync("Error", "Chat room not found.");
                return;
            }

            Console.WriteLine($"Invoke JoinRoom by {Context.ConnectionId} RoomName: {roomName}");

            // Add user to group
            await Groups.AddToGroupAsync(Context.ConnectionId, strRoomId);
            // Send message to group
            await Clients.Group(strRoomId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {roomId}.");
            // Send room name back to client
            await Clients.Caller.SendAsync("RoomName", roomName);
        }
    }
}

