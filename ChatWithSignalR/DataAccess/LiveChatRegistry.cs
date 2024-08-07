using ChatWithSignalR.DTOs;

namespace ChatWithSignalR.DataAccess;

public class LiveChatRegistry
{
    private readonly Dictionary<string, List<MessageDto>> _dictionary;

    public List<MessageDto> GetMessages(RoomRequest roomRequest)
    {
        return _dictionary[roomRequest.Room];
    }

    public void AddMessage(MessageDto message,string receiver)
    {
        _dictionary[receiver].Add(message);
    }

    public bool GroupExists(string room)
    {
        return _dictionary.ContainsKey(room);
    }

    public void CreateGroup(string roomName)
    {
        _dictionary.Add(roomName,new List<MessageDto>());
    }
}