using System.Security.Claims;
using ChatWithSignalR.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

public class LiveChatHub:Hub
{
    private readonly Dictionary<string, List<MessageDto>> _dictionary;

    public async Task SendLiveMessage(InputMessage message)
    {
        var userName = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        
    }
}