using System.Security.Claims;
using ChatWithSignalR.DataAccess;
using ChatWithSignalR.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AuthenticationService = ChatWithSignalR.DataAccess.AuthenticationService;

namespace ChatWithSignalR;

//[Authorize]
[Route("Chat")]
public class ChatController : Controller
{
    //private readonly AuthenticationService _service;
    private readonly LiveChatRegistry _registry;

    public ChatController(/*AuthenticationService service,*/LiveChatRegistry chatRegistry)
    {
        //_service = service;
        _registry = chatRegistry;
    }

    [AllowAnonymous]
    [HttpGet("/auth")]
    public IActionResult Authenticate(string username)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, username),
        };

        var identity = new ClaimsIdentity(claims, "Cookie");
        var principal = new ClaimsPrincipal(identity);

        HttpContext.SignInAsync("Cookie", principal).Wait();
        //return LocalRedirect()
        return Ok();
    }

    [HttpGet("/create")]
    public IActionResult Creategroup(string name)
    {
        _registry.CreateGroup(name);
        return Ok();
    }
    
    /*[HttpPost(nameof(LogIn))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LogIn(LogInDto logInDto,string room)
    {
       var token = await _service.LogIn(logInDto,room);
       return Ok(token);
    }*/
    //[HttpGet("/create")]
    
    

    /*[HttpPost(nameof(Register))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        await _service.Register(userDto);
        return Ok();
    }*/
}