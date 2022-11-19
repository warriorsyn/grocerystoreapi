using GroceryStoreApi.Dto;
using GroceryStoreApi.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly DataContext _dataContext;

        public OrderController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }   

        [HttpGet("{userId}")]
        public async Task<IEnumerable<OrderDto>> GetByUserId(long userId)
        {
            var orders = await _dataContext.Orders
                .Include(i => i.User)
                .Include(i => i.Product)
                .Where(w => w.UserId == userId)
                .OrderBy(w => !w.IsClosed)
                .ToListAsync();


            return orders.Select(item => new OrderDto
            {
                Id = item.Id,
                IsClosed = item.IsClosed,
                CreatedAt = item.CreatedAt,
                User = new UserDto(item.User.Id, item.User.Name, item.User.Email),
                Product = new ProductDto(item.Product),
            }) ;
        }
    }
}
