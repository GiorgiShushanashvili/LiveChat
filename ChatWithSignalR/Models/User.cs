namespace ChatWithSignalR.Models;

public record User(int Id,string UserName,List<UserMessage> Messages,List<Room>? Rooms,int UserProfielId);