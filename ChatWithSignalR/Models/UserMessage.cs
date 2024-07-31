using ChatWithSignalR.DTOs;

namespace ChatWithSignalR.Models;

public record UserMessage(int Id, string Message, string? RoomId, string? ReceiverId, DateTimeOffset SentAt);