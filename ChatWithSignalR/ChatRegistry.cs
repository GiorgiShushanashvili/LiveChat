using ChatWithSignalR.Database;
using ChatWithSignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatWithSignalR;

public class ChatRegistry
{
    private readonly ChatDbContext _chatDbContext;

    public ChatRegistry(ChatDbContext dbContext)
    {
        _chatDbContext = dbContext;
    }

    public void CreateRoom(Room room)
    {
        _chatDbContext.Room.Add(room);
    }

    public void AddMessage(UserMessage message)
    {
        _chatDbContext.Messages.Add(message);
    }

    public List<UserMessage> GetMessages(string room)
    {
        return _chatDbContext.Room.Where(x=>x.Name==room).SelectMany(x=>x.Messages).ToList();
    }

    public int GetUserId(string name)
    {
       return _chatDbContext.User.FirstOrDefault(x => x.UserName == name).Id;
    }

    public int GetRoomId(string name)
    {
        return _chatDbContext.Room.FirstOrDefault(x => x.Name == name).Id;
    }
    
    public IQueryable GetRooms()
    {
        return _chatDbContext.Room.Select(x=>x.Name);
    }
}