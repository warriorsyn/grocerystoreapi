using GroceryStoreApi.Api.Authentication;
using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GroceryStoreApi.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if (usr == null)
            {
                return NotFound();
            }

            return new UserDto(usr);
        }

        [HttpPost]
        public async Task<ActionResult> Store([FromBody] UserDto user)
        {
            await _context.Users.AddAsync(new Domain.User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserDto user)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            usr.Name = user.Name;
            usr.Email = user.Email;
            usr.Password = user.Password;
            usr.IsAdmin = user.IsAdmin;

            _context.Entry(usr).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if (usr == null)
            {
                return NotFound();
            }

            _context.Remove(usr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
