using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }


    // READ: GET /api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }

    // CREATE: POST /api/Category
    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto == null)
        {
            return BadRequest(new { message = "Invalid category data." });
        }

        var category = new Category
        {
            Name = createCategoryDto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category created successfully.", categoryId = category.Id });
    }



    // READ: GET /api/Category/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        return Ok(category);
    }

    // UPDATE: PUT /api/Category/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        if (updateCategoryDto == null)
        {
            return BadRequest(new { message = "Invalid category data." });
        }

        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        category.Name = updateCategoryDto.Name;

        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category updated successfully." });
    }

    // DELETE: DELETE /api/Category/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(new { message = "Category not found." });
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Category deleted successfully." });
    }
}
