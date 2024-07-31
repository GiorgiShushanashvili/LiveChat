namespace ChatWithSignalR.DTOs;

public record MessageDto(string Message,int? RoomId,int? ReceiverId,DateTimeOffset SentAt);