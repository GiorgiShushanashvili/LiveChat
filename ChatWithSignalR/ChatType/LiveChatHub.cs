using System.Security.Claims;
using ChatWithSignalR.DataAccess;
using ChatWithSignalR.DTOs;
using ChatWithSignalR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

[Authorize]
public class LiveChatHub:Hub
{
    private readonly LiveChatRegistry _chatRegistry;

    public async Task SendLiveMessage(InputMessage message)
    {
        var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        var messageToSend = new UserMessage(
            new User(Context.UserIdentifier,userName),
            Message: message.Message,
            Room:message.Room,
            SentAt: DateTimeOffset.Now
        ); 
        //_chatRegistry.AddMessage(messageToSend,message.Room);
        await Clients.All.SendAsync("SendLiveMessage", messageToSend, message.Room);
    }

    public async Task JoinGroup(RoomRequest roomRequest)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomRequest.Room,CancellationToken.None); 
        _chatRegistry.GetMessages(roomRequest);
    }

    public async Task LeaveGroup(RoomRequest roomRequest)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomRequest.Room, CancellationToken.None);
    }

    public async Task SendLiveMessageToGroup(InputMessage inputMessage)
    {
        var userName = Context.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        var messageToSend = new UserMessage(
            new User(Context.UserIdentifier,userName),
            Message: inputMessage.Message,
            Room:inputMessage.Room,
            SentAt: DateTimeOffset.Now);
        /*if(!_chatRegistry.GroupExists(inputMessage.Room))
            _chatRegistry.CreateGroup(inputMessage.Room);*/
        _chatRegistry.AddMessage(messageToSend,inputMessage.Room);
        await Clients.GroupExcept(inputMessage.Room,new[]{ Context.ConnectionId})
            .SendAsync("SendLiveMessageToGroup", messageToSend);
    }
}