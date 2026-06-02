using Api.DTOs;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserRequest> _validator;
    private readonly IValidator<LoginRequest> _loginValidator;

    public UsersController(
    IUserService userService,
    IValidator<CreateUserRequest> createUserValidator,
    IValidator<LoginRequest> loginValidator)
    {
        _userService = userService;
        _validator = createUserValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

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
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var user = await _userService.LoginAsync(request);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}