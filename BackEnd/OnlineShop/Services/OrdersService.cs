using BackEnd.Interfaces.Repositories;
using BackEnd.Interfaces.Services;
using OnlineShop.DTO.Order;
using OnlineShop.Entities;
using OnlineShop.Models;
using OnlineShop.Repositories;

namespace BackEnd.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IProductsRepository _productsRepository;

        public OrdersService(IOrdersRepository ordersRepository, IUsersRepository usersRepository, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _usersRepository = usersRepository;
            _productsRepository = productsRepository;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync(int userId)
        {
            var orders = await _ordersRepository.GetAllAsync(userId);

            if (orders == null)
            {
                return new List<OrderDto>();
            }

            var ordersDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                ordersDto.Add(new OrderDto
                {
                    UserName = order.User?.Name ?? "Unknown",
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.Status,
                    ProductId = order.ProductId,
                    ProductName = order.Product?.Name ?? "Unknown"
                });
            }

            return ordersDto;
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            var orderDto = new OrderDto
            {
                UserName = order.User?.Name ?? "Unknown",
                OrderId = order.Id,
                OrderStatus = order.Status,
                OrderDate = order.OrderDate,
                ProductId = order.ProductId,
                ProductName = order.Product?.Name ?? "Unknown"
            };

            return orderDto;
        }

        public async Task<ServiceResult> CreateOrderAsync(int userId, int productId, DateTime dateTime)
        {
            var result = new ServiceResult();

            var existingUser = await _usersRepository.GetByIdAsync(userId);
            var existingProduct = await _productsRepository.GetByIdAsync(productId);

            if (existingUser == null)
            {
                result.Errors.Add("User not found.");
            }

            else if (existingProduct == null)
            {
                result.Errors.Add("Product not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            var newOrder = new CreateOrderDto
            {
                UserId = userId,
                ProductId = productId,
                OrderDate = dateTime,
                Status = StatusEnum.Pending
            };

            await _ordersRepository.AddAsync(newOrder);

            return result;
        }

        public async Task<ServiceResult> UpdateOrderAsync(int orderId, StatusEnum status)
        {
            var result = new ServiceResult();

            var existingOrder = await _ordersRepository.GetByIdAsync(orderId);

            if (existingOrder == null)
            {
                result.Errors.Add("Order not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            var newOrder = new UpdateOrderDto
            {
                Id = orderId,
                Status = status
            };

            await _ordersRepository.UpdateByIdAsync(newOrder);

            return result;
        }

        // Useless method by relations, commented out for now
        /*public async Task<ServiceResult> UpdateOrderAsync(int userId, int productId, StatusEnum status)
        {
            var result = new ServiceResult();

            var existingOrder = await _ordersRepository.GetByIdAsync(userId, productId);

            if (existingOrder == null)
            {
                result.Errors.Add("Order not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            var newOrder = new UpdateOrderDto
            {
                UserId = userId,
                ProductId = productId,
                Status = status
            };

            await _ordersRepository.UpdateAsync(newOrder);

            return result;
        }

        public async Task<ServiceResult> DeleteOrderAsync(int userId, int productId)
        {
            var result = new ServiceResult();

            var existingOrder = await _ordersRepository.GetByIdAsync(userId, productId);

            if (existingOrder == null)
            {
                result.Errors.Add("Order not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            await _ordersRepository.DeleteAsync(userId, productId);

            return result;
        }*/

        public async Task<ServiceResult> DeleteOrderAsync(int orderId)
        {
            var result = new ServiceResult();

            var existingOrder = await _ordersRepository.GetByIdAsync(orderId);

            if (existingOrder == null)
            {
                result.Errors.Add("Order not found.");
            }

            if (!result.isSuccess)
            {
                return result;
            }

            await _ordersRepository.DeleteAsync(orderId);

            return result;
        }
    }
}
