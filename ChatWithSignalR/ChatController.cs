using ChatWithSignalR.DTOs;
using Microsoft.AspNetCore.Mvc;

using AuthenticationService = ChatWithSignalR.DataAccess.AuthenticationService;

namespace ChatWithSignalR;

public class ChatController : ControllerBase
{
    private readonly AuthenticationService _service;

    public ChatController(AuthenticationService service)
    {
        _service = service;
    }

    [HttpPost(nameof(LogIn))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LogIn(LogInDto logInDto,string room)
    {
       var token = await _service.LogIn(logInDto,room);
       return Ok(token);
    }
    [HttpGet]
    

    [HttpPost(nameof(Register))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        await _service.Register(userDto);
        return Ok();
    }
}