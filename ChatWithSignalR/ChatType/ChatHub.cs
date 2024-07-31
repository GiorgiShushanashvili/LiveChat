using ChatWithSignalR.DTOs;
using ChatWithSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

public class ChatHub:Hub
{
    private readonly ChatRegistry _chatRegistry;

    public ChatHub(ChatRegistry chatRegistry)
    {
        _chatRegistry = chatRegistry;
    }
    public async Task SendMessage(InputMessage message)
    {
        var userName = Context.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
        var userMessage = new UserMessage(0,message.Message,RoomId:null,Context.UserIdentifier,DateTimeOffset.Now);
        _chatRegistry.AddMessage(userMessage);
        await Clients.All.SendAsync("ReceiveMessage",message,userMessage);
    } 

    public async Task<List<OutputMessage>> JoinRoom(RoomRequest request)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, request.Room);

        return _chatRegistry.GetMessages(request.Room).ToList();
    }
}