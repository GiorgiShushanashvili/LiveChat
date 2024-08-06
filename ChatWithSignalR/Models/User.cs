using ChatWithSignalR.Enums;

namespace ChatWithSignalR.Models;

public record User(int Id,string UserName,byte[] Password,
    List<UserMessage>? Messages,List<Room>? Rooms,int UserProfielId,Dictionary<string,Roles>? GroupRoles);