using ChatWithSignalR.Enums;

namespace ChatWithSignalR.DTOs;

public record UserDto(string FullName,string Password,string Email,string UserName,Roles Roles);