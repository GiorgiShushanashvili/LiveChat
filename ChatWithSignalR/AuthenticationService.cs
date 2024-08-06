using ChatWithSignalR.Database;
using ChatWithSignalR.DTOs;
using ChatWithSignalR.Enums;
using ChatWithSignalR.Exceptions;
using ChatWithSignalR.Models;
using ChatWithSignalR.Security;
using Microsoft.EntityFrameworkCore;

namespace ChatWithSignalR;

public class AuthenticationService
{
    private readonly ChatDbContext _dbContext;
    private readonly PasswordManagement _passwordManagement;
    private readonly TokenManagement _tokenManagement;

    public AuthenticationService(ChatDbContext dbContext,PasswordManagement passwordManagement,TokenManagement tokenManagement)
    {
        _dbContext = dbContext;
        _tokenManagement = tokenManagement;
        _passwordManagement = passwordManagement;
    }

    public async Task Register(UserDto userDto)
    {
        if (_dbContext.User.Any(x => x.UserName == userDto.UserName) ||
            _dbContext.UserProfile.Any(x => x.Email == userDto.Email))
        {
            throw new UserAlreadyExistsException();
        }

        var passwordHash = _passwordManagement.GetPasswordHash(userDto.Password);
        var userProfileToAdd = new UserProfile(
            Id:default,
            FullName: userDto.FullName,
            Email: userDto.Email,
            User:null
        );
        
        var userToAdd = new User(
            Id:default,
            UserName: userDto.UserName,
            Password: passwordHash,
            Messages:null,
            Rooms:null,
            UserProfielId:userProfileToAdd.Id,
            GroupRoles: null
            );
        await _dbContext.User.AddAsync(userToAdd);
        await _dbContext.UserProfile.AddAsync(userProfileToAdd);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> LogIn(LogInDto logInDto,string? room)
    {
        if (string.IsNullOrEmpty(logInDto.UserName) || string.IsNullOrEmpty(logInDto.Password))
            throw new ArgumentException("Fill The Empty Gaps");
        var user = await _dbContext.User.FirstOrDefaultAsync(x => x.UserName == logInDto.UserName);
        if (user == null)
            throw new UserNotExistsException();
        
        var passwordHash = _passwordManagement.GetPasswordHash(logInDto.Password);
        var checkPassword = _passwordManagement.IsValidPassowrHash(user.Password,passwordHash);
        if (!checkPassword)
            throw new ArgumentException("Incorrect password,Try Again");
        var token = await _tokenManagement.GenerateToken(user.GroupRoles[room].ToString(), user.UserName);
        return token;
    }
}