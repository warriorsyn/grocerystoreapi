using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

        [HttpPost]
        public async Task<ActionResult> Store([FromBody] UserDto user)
        {
            await _context.Users.AddAsync(new Domain.User { Name = user.Name, Email = user.Email, Password = user.Password, IsAdmin = user.IsAdmin });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserDto user)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (usr == null)
            {
                return NotFound();
            }

            usr.Name = user.Name;
            usr.Email = user.Email;
            usr.Password = user.Password;
            usr.IsAdmin = user.IsAdmin;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
