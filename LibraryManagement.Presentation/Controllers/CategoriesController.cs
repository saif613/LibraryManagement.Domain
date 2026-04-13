using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [AllowAnonymous]
    [HttpGet] 
    public async Task<IActionResult> GetAllPaged([FromQuery] int page = 1, CancellationToken ct = default)
    {
        var result = await _categoryService.GetCategoriesPagedAsync(page, ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id, ct);
        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(CreateCategoryRequest request, CancellationToken ct)
    {
        var result = await _categoryService.CreateCategoryAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, CreateCategoryRequest request, CancellationToken ct)
    {
        await _categoryService.UpdateCategoryAsync(id, request, ct);
        return NoContent(); 
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _categoryService.SoftDeleteCategoryAsync(id, ct);
        return NoContent();
    }

    [HttpGet("search")]
    [AllowAnonymous] 
    public async Task<IActionResult> SearchSingle([FromQuery] string name, CancellationToken ct)
    {
        var result = await _categoryService.SearchOneItemByNameAsync(name, ct);
        return Ok(result);
    }
}