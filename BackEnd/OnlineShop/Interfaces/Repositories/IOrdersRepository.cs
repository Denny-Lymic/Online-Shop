using OnlineShop.DTO.Order;
using OnlineShop.Entities;

namespace BackEnd.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        Task AddAsync(CreateOrderDto orderDto);
        Task DeleteAsync(int orderId);
        Task DeleteAsync(int userId, int productId);
        Task<List<OrderEntity>> GetAllAsync(int userId);
        Task<OrderEntity?> GetByIdAsync(int orderId);
        Task UpdateAsync(UpdateOrderDto updateOrderDto);
        Task UpdateByIdAsync(UpdateOrderDto updateOrderDto);
    }
}