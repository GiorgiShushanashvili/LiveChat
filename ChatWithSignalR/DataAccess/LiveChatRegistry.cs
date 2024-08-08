using ChatWithSignalR.DTOs;
using ChatWithSignalR.Models;

namespace ChatWithSignalR.DataAccess;

public class LiveChatRegistry
{
    private readonly Dictionary<string, List<UserMessage>> _dictionary;

    public LiveChatRegistry()
    {
        _dictionary = new Dictionary<string, List<UserMessage>>();
    }
    public List<UserMessage> GetMessages(RoomRequest roomRequest)
    {
        return _dictionary[roomRequest.Room];
    }
    
    
    public void AddMessage(UserMessage message,string receiver)
    {
        _dictionary[receiver].Add(message);
    }

    public bool GroupExists(string room)
    {
        return _dictionary.ContainsKey(room);
    }

    public void CreateGroup(string roomName)
    {
        _dictionary.Add(roomName,new List<UserMessage>());
    }
}