using System.Security.Claims;
using ChatWithSignalR.DataAccess;
using ChatWithSignalR.DTOs;
using ChatWithSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

/*public class ChatHub:Hub
{
    private readonly ChatRegistry _chatRegistry;

    public ChatHub(ChatRegistry chatRegistry)
    {
        _chatRegistry = chatRegistry;
    }
    public async Task SendMessage(InputMessage message)
    {
        var receiverId = _chatRegistry.GetUserId(message.Room);
        var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        var userMessage = new UserMessage(0,message.Message,RoomId:null,receiverId,DateTimeOffset.Now,null);
        _chatRegistry.AddMessage(userMessage);
        await Clients.All.SendAsync("ReceiveMessage",userName,userMessage);
    } 
}*/