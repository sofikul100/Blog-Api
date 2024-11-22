using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstApplication.Models;


namespace FirstApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _context.Comments
                                          .Include(c => c.User)  // Include user data
                                          .Include(c => c.Post)  // Include post data
                                          .ToListAsync();

            return Ok(comments);
        }

        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _context.Comments
                                         .Include(c => c.User)
                                         .Include(c => c.Post)
                                         .Where(c => c.Id == id)
                                         .FirstOrDefaultAsync();

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDto commentCreateDto)
        {
            if (commentCreateDto == null)
            {
                return BadRequest();
            }

            var comment = new Comment
            {
                Content = commentCreateDto.Content,
                UserId = commentCreateDto.UserId,
                PostId = commentCreateDto.PostId,
                CreatedAt = commentCreateDto.CreatedAt
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto commentUpdateDto)
        {
            if (id != commentUpdateDto.PostId)  // Ensure the ID matches
            {
                return BadRequest();
            }

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Content = commentUpdateDto.Content;
            existingComment.UserId = commentUpdateDto.UserId;
            existingComment.PostId = commentUpdateDto.PostId;

            _context.Entry(existingComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
