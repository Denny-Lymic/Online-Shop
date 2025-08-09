using BackEnd.Interfaces.Services;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Entities;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("{orderId:int}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var order = await _ordersService.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet]
        [Route("AllOrders/{userId:int}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var order = await _ordersService.GetAllOrdersAsync(userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [Route("{userId:int}/{productId:int}")]
        public async Task<IActionResult> CreateOrder(int userId, int productId)
        {
            var result = await _ordersService.CreateOrderAsync(userId, productId, DateTime.UtcNow);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Order created successfully." });
        }

        [HttpPatch]
        [Route("{orderId:int}/{status}")]
        public async Task<IActionResult> UpdateOrder(int orderId, StatusEnum status)
        {
            var result = await _ordersService.UpdateOrderAsync(orderId, status);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Order updated successfully." });
        }

        // Useless methods by relations, as we are using orderId for deletion
        /*[HttpPut]
        [Route("{userId:int}/{productId:int}")]
        public async Task<IActionResult> UpdateOrder(int userId, int productId, StatusEnum status)
        {
            var result = await _ordersService.UpdateOrderAsync(userId, productId, status);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Order updated successfully." });
        }

        [HttpDelete]
        [Route("{userId:int}/{productId:int}")]
        public async Task<IActionResult> DeleteOrder(int userId, int productId)
        {
            var result = await _ordersService.DeleteOrderAsync(userId, productId);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Order deleted successfully." });
        }*/

        [HttpDelete]
        [Route("{orderId:int}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var result = await _ordersService.DeleteOrderAsync(orderId);

            if (!result.isSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Order deleted successfully." });
        }
    }
}
