using ChatWithSignalR.Models;

namespace ChatWithSignalR.DTOs;

public record OutputMessage(string Message,User User,string Room,DateTimeOffset SentAt);