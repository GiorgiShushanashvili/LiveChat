namespace ChatWithSignalR.Models;

public record Room(int Id,string Name,List<User> Users,List<UserMessage>? Messages);