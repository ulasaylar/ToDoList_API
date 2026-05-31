using Api.DTOs;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = await _userService.CreateUserAsync(request);

            return Created("", new
            {
                user.Id,
                user.Username,
                user.Email
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }
}