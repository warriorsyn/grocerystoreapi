using GroceryStoreApi.Api.Authentication;
using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<JwtAuthenticationResponse>> Authenticate([FromBody] JwtAuthenticationRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(w => w.Email == req.Email);

            if (user == null)
            {
                return BadRequest("Check your email and password!");
            }

            if (user.Password != req.Password)
            {
                return BadRequest("Check your email and password!");
            }

            var token = JwtAuthenticationManager.Authenticate(user);

            return new JwtAuthenticationResponse
            {
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                Token = token
            };
        }
    }
}
