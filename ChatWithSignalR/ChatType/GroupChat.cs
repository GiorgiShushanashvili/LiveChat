using ChatWithSignalR.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace ChatWithSignalR.ChatType;

public class GroupChat:Hub
{
    private readonly ChatRegistry _chatRegistry;

    public GroupChat(ChatRegistry chatRegistry)
    {
        _chatRegistry = chatRegistry;
    }

    public async Task<List<OutputMessage>> AddToGroup(RoomRequest room,string userName)
    {
        var connId=_chatRegistry
        await Groups.AddToGroupAsync(Context.ConnectionId,room.Room)
    }

}