using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

     // READ: GET /api/Post
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _context.Posts.Include(p => p.Comments).ToListAsync();

        return Ok(posts);
    }

    // CREATE: POST /api/Post
    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostDto createPostDto)
    {
        if (createPostDto == null)
        {
            return BadRequest(new { message = "Invalid post data." });
        }

        var post = new Post
        {
            Title = createPostDto.Title,
            Content = createPostDto.Content,
            CreatedAt = DateTime.UtcNow,
            UserId = createPostDto.UserId,
            CategoryId = createPostDto.CategoryId
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Post created successfully.", postId = post.Id });
    }

   

    // READ: GET /api/Post/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound(new { message = "Post not found." });
        }

        return Ok(post);
    }

    // UPDATE: PUT /api/Post/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
    {
        if (updatePostDto == null)
        {
            return BadRequest(new { message = "Invalid post data." });
        }

        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound(new { message = "Post not found." });
        }

        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        post.CategoryId = updatePostDto.CategoryId;

        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Post updated successfully." });
    }

    // DELETE: DELETE /api/Post/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound(new { message = "Post not found." });
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Post deleted successfully." });
    }
}
