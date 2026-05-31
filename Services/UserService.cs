using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Services.Interfaces;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class UserService : IUserService
{
    private readonly ApiContext _context;

    public UserService(ApiContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        bool usernameExists = await _context.Users
            .AnyAsync(x => x.Username == request.Username);

        if (usernameExists)
            throw new Exception("Username already exists.");

        bool emailExists = await _context.Users
            .AnyAsync(x => x.Email == request.Email);

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
}