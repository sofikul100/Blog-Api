using Microsoft.AspNetCore.Mvc;
using FirstApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace FirstApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers (){
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
        {
            
            if (createUserDto == null)
            {
                return BadRequest(new { message = "Invalid user data." });
            }

            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { MessageContent = "User Created Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error occurred", details = ex.Message });
            }
        }


        // READ: GET /api/User/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
    {
        var user = await _context.Users
        .Include(u => u.Posts).ThenInclude(p=>p.Category)
        .Include(u => u.Comments) 
        
        .FirstOrDefaultAsync(u => u.Id == id); 

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        return Ok(user);
    }

    // UPDATE: PUT /api/User/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        if (updateUserDto == null)
        {
            return BadRequest(new { message = "Invalid user data." });
        }

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        user.Username = updateUserDto.Username;
        user.Email = updateUserDto.Email;

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { message = "User updated successfully." });
    }

    // DELETE: DELETE /api/User/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User deleted successfully." });
    }



    }
}
