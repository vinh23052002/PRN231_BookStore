using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sever.DTO;
using Sever.Models;

namespace Sever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBContext _context;

        public UsersController(DBContext context)
        {
            _context = context;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static LoginResponse UserToLoginResponse(User user)
        {
            return new LoginResponse()
            {
                user_id = user.user_id,
                password = user.password,
                email_address = user.email_address,
                first_name = user.first_name,
                last_name = user.last_name,
                middle_name = user.middle_name,
                role_id = user.role_id,
                source = user.source,
                pub_id = user.pub_id,

            };
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var res = await _context.Users
                .Select(p => UserToLoginResponse(p))
                .ToListAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginResponse>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.SingleOrDefaultAsync(p => p.user_id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(UserToLoginResponse(user));
        }
        // GET: api/Users/5
        [HttpGet("/Login")]
        public async Task<ActionResult<User>> Login([FromQuery] LoginRequest loginRequest)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.SingleOrDefaultAsync(p => p.email_address.Equals(loginRequest.email) && p.password.Equals(loginRequest.password));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(UserToLoginResponse(user));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/Resgister")]
        public async Task<ActionResult<User>> Register(RegisterRequest req)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DBContext.Users'  is null.");
            }
            if (!req.confirmPassword.Equals(req.password))
            {
                return BadRequest("Password and Confirm Password not equal");
            }
            var user = await _context.Users.FirstOrDefaultAsync(p => p.email_address.Equals(req.email));
            if (user != null)
            {
                return BadRequest("This email existed!!!");
            }
            var newUser = new User()
            {
                email_address = req.email,
                password = req.password,
                pub_id = 1,
                role_id = 2
            };
            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(UserToLoginResponse(newUser));
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserRequest req)
        {

            var user = await _context.Users.FirstOrDefaultAsync(p => p.user_id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.first_name = req.first_name;
            user.middle_name = req.middle_name;
            user.last_name = req.last_name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.user_id == id)).GetValueOrDefault();
        }
    }
}
