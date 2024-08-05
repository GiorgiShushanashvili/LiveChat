using ChatWithSignalR.Database;
using ChatWithSignalR.DTOs;
using ChatWithSignalR.Enums;
using ChatWithSignalR.Models;

namespace ChatWithSignalR;

public class AuthenticationService
{
    private readonly ChatDbContext _dbContext;

    public AuthenticationService(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Register(UserDto userDto)
    {
        var userProfileToAdd = new UserProfile(
            FullName: userDto.FullName,
            Email: userDto.Email,User:null
        );
        
        var userToAdd = new User(
            UserName: userDto.UserName,
            Password: userDto.Password,
            RoleId: Roles.User.ToString(),
            UserProfielId:userProfileToAdd.Id,
            Messages:null,Rooms:null);
        
    }
}