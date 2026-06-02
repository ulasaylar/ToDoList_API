using Api.DTOs;
using Api.Models;

namespace Api.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}