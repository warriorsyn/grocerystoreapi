using GroceryStoreApi.Domain;
using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Index()
        {
            var products = await _context.Products.ToListAsync();
              
            return products.Select(item => new ProductDto(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return new ProductDto(product);
        }

        [HttpPost]
        public async Task<ActionResult> Store([FromBody] ProductDto body)
        {
            var product = new Product
            {
                Name = body.Name,
                Description = body.Description,
                Price = body.Price,
                Stock = body.Stock,
                CategoryId = body.CategoryId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDto body)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Entry(product).CurrentValues.SetValues(body);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }


            _context.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}



