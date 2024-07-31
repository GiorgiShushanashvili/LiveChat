namespace ChatWithSignalR.DTOs;

public record OutputMessage(string Message,string UserName,string Room,DateTimeOffset SentAt);