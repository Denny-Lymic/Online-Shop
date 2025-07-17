using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTO.Order;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class OrdersRepository
    {
        private readonly ShopDbContext _context;

        public OrdersRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderEntity>> GetAllAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Product)
                .Include(o => o.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OrderEntity?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.Product)
                .Include(o => o.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateOrderDto orderDto)
        {
            var order = new OrderEntity
            {
                UserId = orderDto.UserId,
                ProductId = orderDto.ProductId,
                OrderDate = orderDto.OrderDate,
                Status = orderDto.Status
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByIdAsync(UpdateOrderDto updateOrderDto)
        {
            await _context.Orders
                .Where(o => o.Id == updateOrderDto.Id)
                .ExecuteUpdateAsync(o => o.SetProperty(x => x.Status, updateOrderDto.Status));
        }

        public async Task UpdateAsync(UpdateOrderDto updateOrderDto)
        {
            await _context.Orders
                .Where(o => o.UserId == updateOrderDto.UserId && o.ProductId == updateOrderDto.ProductId)
                .ExecuteUpdateAsync(o => o.SetProperty(x => x.Status, updateOrderDto.Status));
        }

        public async Task DeleteAsync(int userId, int productId)
        {
            await _context.Orders
                .Where(o => o.UserId == userId && o.ProductId == productId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            await _context.Orders
                .Where(o => o.Id == orderId)
                .ExecuteDeleteAsync();
        }
    }
}
