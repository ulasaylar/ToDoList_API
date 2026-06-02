using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Services.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services;

public class UserService : IUserService
{
    private readonly ApiContext _context;
    private readonly IConfiguration _config;

    public UserService(ApiContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        bool usernameExists = await _context.Users.AnyAsync(x => x.Username == request.Username);

        if (usernameExists)
            throw new Exception("Username already exists.");

        bool emailExists = await _context.Users.AnyAsync(x => x.Email == request.Email);

        if (emailExists)
            throw new Exception("Email already exists.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x =>
                x.Username == request.UsernameOrEmail ||
                x.Email == request.UsernameOrEmail);

        if (user == null)
            throw new Exception("User not found.");

        bool passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!passwordValid)
            throw new Exception("Invalid password.");

        var token = GenerateToken(user);

        return new LoginResponse
        {
            Token = token
        };
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}