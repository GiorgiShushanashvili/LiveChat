using System.Collections.Concurrent;
using ChatWithSignalR.DTOs;
using ChatWithSignalR.Exceptions;
using ChatWithSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

public class GroupChat:Hub
{
    private readonly ConcurrentDictionary<string, string> _currentConnections = new();
    private readonly ChatRegistry _chatRegistry;

    public GroupChat(ChatRegistry chatRegistry)
    {
        _chatRegistry = chatRegistry;
    }

    public async Task OnConnectedAsync()
    {
        var userName = Context.User.Identity.Name;
        _currentConnections.TryAdd(userName, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public async Task<List<OutputMessage>> AddToGroup(RoomRequest room,string userName)
    {
        List<OutputMessage> messages = new();
        if (Context.User.HasClaim(c => c.Type == "Role" && c.Value == "Admin"))
        {
            if (_currentConnections.TryGetValue(userName, out string connId))
            {
                await Groups.AddToGroupAsync(connId, room.Room);
                Clients.Group(room.Room).SendAsync("Notify", $"{userName} has joined"); 
                messages = _chatRegistry.GetMessages(room.Room).Select(x => new OutputMessage(
                    Message: x.Message,
                    UserName: x.User.UserName,
                    Room: room.Room,
                    SentAt: x.SentAt)).ToList();
            }
            else
            {
                throw new UserNotAddException();
            }
        }
        return messages;
    }

    public async Task LeaveRoom(RoomRequest room)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Room);
    }

    public async Task RemoveFromGroup(RoomRequest room, string userName)
    {
        if (Context.User.HasClaim(x => x.Type == "Type" && x.Value == "Admin"))
        {
            if (_currentConnections.TryGetValue(userName, out string connId))
            {
                await Groups.RemoveFromGroupAsync(connId, room.Room);
                Clients.Group(room.Room).SendAsync("RemoveUserFromGroup", $"{userName} Has Been Removed");
            }
        }
    }

    public async Task SendMessageToGroup(InputMessage inputMessage)
    {
        var userName = Context.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
        var roomId = _chatRegistry.GetRoomId(inputMessage.Room);
        var userMessage = new UserMessage(0, inputMessage.Message, roomId, null, DateTimeOffset.Now, null);
        _chatRegistry.AddMessage(userMessage); 
        await Clients.GroupExcept(inputMessage.Room, new[] { Context.ConnectionId }).SendAsync("Send_MessageToGroup",userMessage,inputMessage.Room);
    }

}