namespace ChatWithSignalR.Models;

public record User(int Id,string UserName,string Password,
    List<UserMessage>? Messages,List<Room>? Rooms,int UserProfielId,string RoleId);