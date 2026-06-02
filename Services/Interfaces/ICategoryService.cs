using Api.Models;
using Api.DTOs;

namespace Api.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetUserCategories(int userId);
    Task<Category> CreateCategory(CreateCategoryRequest request, int userId);
    Task<bool> DeleteCategory(int categoryId, int userId);
}