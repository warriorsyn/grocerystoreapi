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

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return new CategoryDto(category);
        }

        [HttpPost]
        public async Task<ActionResult> Store([FromBody] CategoryDto category)
        {
            await _context.Categories.AddAsync(new Domain.Category { Description = category.Description, Name = category.Name });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CategoryDto category) {
            var cat = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (cat == null)
            {
                return NotFound();
            }
            
            _context.Entry(cat).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
