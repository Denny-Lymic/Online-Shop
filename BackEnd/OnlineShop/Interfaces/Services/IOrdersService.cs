using BackEnd.DTO.Order;
using BackEnd.Entities;
using BackEnd.Models;

namespace BackEnd.Interfaces.Services
{
    public interface IOrdersService
    {
        Task<ServiceResult> CreateOrderAsync(int userId, int productId, DateTime dateTime);
        Task<ServiceResult> DeleteOrderAsync(int orderId);
        Task<List<OrderDto>> GetAllOrdersAsync(int userId);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        Task<ServiceResult> UpdateOrderAsync(int orderId, StatusEnum status);
    }
}