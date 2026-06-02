using Api.Data;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Api.DTOs;

namespace Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ApiContext _context;

    public CategoryService(ApiContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetUserCategories(int userId)
    {
        return await _context.Categories
            .Where(x => x.UserId == userId)
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<Category> CreateCategory(CreateCategoryRequest request, int userId)
    {
        var category = new Category
        {
            Name = request.Name,
            UserId = userId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteCategory(int categoryId, int userId)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == categoryId && x.UserId == userId);

        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
}