using ChatWithSignalR.Database;
using ChatWithSignalR.DTOs;
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

    public void AddMessage(UserMessage message)
    {
        _chatDbContext.Messages.Add(message);
        _chatDbContext.SaveChanges();
    }

    public List<UserMessage> GetMessages(string room)
    {
        return _chatDbContext.Room.Where(x=>x.Name==room).SelectMany(x=>x.Messages).ToList();
    }

    public int GetUserId(string name)
    {
       return _chatDbContext.User.FirstOrDefault(x => x.UserName == name).Id;
    }

    public User GetUserByName(string name)
    {
       return _chatDbContext.User.FirstOrDefault(x => x.UserName == name);
    }

    public int GetRoomId(string name)
    {
        return _chatDbContext.Room.FirstOrDefault(x => x.Name == name).Id;
    }

    public void CreateGroup(Room room)
    {
        _chatDbContext.Room.Add(room);
        _chatDbContext.SaveChanges();
    }
    
    public IQueryable<string> GetRooms()
    {
        return _chatDbContext.Room.Select(x=>x.Name);
    }
}