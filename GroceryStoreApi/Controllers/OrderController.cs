using GroceryStoreApi.Domain;
using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<OrderDto>> GetByLoggedUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _dataContext.Orders
              .Include(i => i.User)
              .Include(i => i.Product)
              .Where(w => w.UserId == long.Parse(userId))
              .OrderBy(w => !w.IsClosed)
              .ToListAsync();


            return orders.Select(item => new OrderDto
            {
                Id = item.Id,
                IsClosed = item.IsClosed,
                CreatedAt = item.CreatedAt,
                User = new UserDto(item.User.Id, item.User.Name, item.User.Email),
                Product = new ProductDto(item.Product),
            });
        }


        [HttpGet("{userId}")]
        public async Task<IEnumerable<OrderDto>> GetByUserId(long userId)
        {
            var orders = await _dataContext.Orders
                .Include(i => i.User)
                .Include(i => i.Product)
                .Where(w => w.UserId == userId)
                .OrderBy(w => w.IsClosed)
                .ToListAsync();


            return orders.Select(item => new OrderDto
            {
                Id = item.Id,
                IsClosed = item.IsClosed,
                CreatedAt = item.CreatedAt,
                User = new UserDto(item.User.Id, item.User.Name, item.User.Email),
                Product = new ProductDto(item.Product),
                Quantity = item.Quantity,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> CloseOrder(long id)
        {
            var order = await _dataContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return BadRequest("Could not find order");
            }

            if (order.IsClosed)
            {
                return BadRequest("This order is already closed");
            }

            order.IsClosed = true;

            _dataContext.Update(order);
            await _dataContext.SaveChangesAsync();
        

            return NoContent();
        }

        [HttpPost("{userId}/{productId}")]
        public async Task<ActionResult> Store(long userId, long productId)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(w => w.Id == productId);

            if (product == null) return BadRequest();

            if (product.Stock == 0)
            {
                return BadRequest("There is no more stock for this product");
            }

            var existingOrder = await _dataContext.Orders.Where(o => o.ProductId == productId && o.UserId == userId && !o.IsClosed).FirstOrDefaultAsync();

            if (existingOrder != null)
            {
                existingOrder.Quantity++;
                product.Stock--;

                _dataContext.Update(existingOrder);
                _dataContext.Update(product);

                await _dataContext.SaveChangesAsync();

                return NoContent();
            }

            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                ProductId = product.Id,
                IsClosed = false,
                Quantity = 1,
            };

            product.Stock--;

            _dataContext.Update(product);
            await _dataContext.AddAsync(order);
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(w => w.Id == id);

            if (order == null) return NotFound();

            var product = await _dataContext.Products.FirstOrDefaultAsync(w => w.Id == order.ProductId);

            product.Stock += order.Quantity;

            _dataContext.Update(product);

            _dataContext.Remove(order);

            await _dataContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
