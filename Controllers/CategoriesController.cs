using Api.DTOs;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyCategories()
    {
        var userId = User.Claims
            .FirstOrDefault(x =>
                x.Type == ClaimTypes.NameIdentifier ||
                x.Type == "sub" ||
                x.Type == "nameid"
            )?.Value;

        if (userId == null)
            return Unauthorized();

        var id = int.Parse(userId);

        var result = await _service.GetUserCategories(id);

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _service.CreateCategory(request, userId);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _service.DeleteCategory(id, userId);

        if (!result)
            return NotFound();

        return Ok();
    }
}