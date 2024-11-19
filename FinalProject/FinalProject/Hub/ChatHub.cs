using FinalProject.Models;
using FinalProject.Helpers;
using Microsoft.AspNetCore.SignalR;

using Microsoft.AspNetCore.Identity;



public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

    public ChatHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }
        private static Dictionary<string, UserConnectionInfo> connectedUsers = new Dictionary<string, UserConnectionInfo>();
        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            string userName = httpContext.Request.Query["customData"].ToString();

            connectedUsers[Context.ConnectionId] = new UserConnectionInfo { ConnectionId = Context.ConnectionId, UserName = userName };

            return base.OnConnectedAsync();
        }

        public string? GetConnectionIdByUserName(string userName)
        {
            foreach (var user in connectedUsers)
            {
                if (user.Value.UserName == userName)
                {
                    return user.Key;
                }
            }
            return null;
        }

        public string? GetUserNameByConnectionId(string connectionId)
        {
            if (connectedUsers.TryGetValue(connectionId, out var userConnectionInfo))
            {
                return userConnectionInfo.UserName;
            }
            return null;
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (connectedUsers.ContainsKey(Context.ConnectionId))
            {
                connectedUsers.Remove(Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(string ReceiverUserName, string? message = null)
        {
            var senderConnectionId = Context.ConnectionId;
            var senderUserName = GetUserNameByConnectionId(senderConnectionId);
            var sender = await _userManager.FindByNameAsync(senderUserName);
             var receiver = await _userManager.FindByNameAsync(ReceiverUserName);

        if ( string.IsNullOrEmpty(message))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "System", "Cannot send an empty message");
                return;
            }

            
            string messageToSend = message ?? "";

            var user = GetConnectionIdByUserName(ReceiverUserName);

            if (string.IsNullOrEmpty(user))
            {
                await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, messageToSend);
            }
            else
            {
                await Clients.Client(user).SendAsync("ReceiveMessage", sender.UserName, messageToSend);
                await Clients.Caller.SendAsync("ReceiveMessage", sender.UserName, messageToSend);
            }

            
                
                var mess = new Chat
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Message = messageToSend,
                    Timestamp = DateTime.UtcNow
                };

                await _context.chats.AddAsync(mess);
           
                await _context.SaveChangesAsync();
        }

    }
