using System.Security.Cryptography.X509Certificates;
using ChatWithSignalR.DTOs;

namespace ChatWithSignalR.Models;

//public record UserMessage(int Id, string Message, int? RoomId, int? ReceiverId, DateTimeOffset SentAt,User User);
public record UserMessage(User User,string Room,string Message,
    DateTimeOffset SentAt);