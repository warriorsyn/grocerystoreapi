using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Index()
        {
            var categories = _context.Categories.Select(c => new CategoryDto { Id = c.Id, Description = c.Description, Name = c.Name });

            return await categories.ToListAsync();
        }
    }
}
