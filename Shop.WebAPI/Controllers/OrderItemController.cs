using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Infrastructure.Commands;
using Shop.Infrastructure.Services;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // /orderItem
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var o = await _orderItemService.BrowseAll();
            return Json(o);
        }

        // /orderItem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            var o = await _orderItemService.Get(id);
            return Json(o);

        }

        // /orderItem/filter?orderId=4
        [HttpGet("filter")]
        public async Task<IActionResult> GetOrderItemByFilter(int orderId)
        {
            var o = await _orderItemService.BrowseAllByFilter(orderId);
            return Json(o);
        }

        // /orderItem
        [HttpPost]
        public async Task<IActionResult> AddOrderItem([FromBody] CreateOrderItem orderItem)
        {
            OrderItem o = new OrderItem()
            {
                Order = orderItem.Order,
                Product = orderItem.Product,
                Quantity = orderItem.Quantity
            };

            await _orderItemService.Add(o);
            return CreatedAtRoute(null, new { id = o.Id }, o);
        }

        // /orderItem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrderItem([FromBody] UpdateOrderItem orderItem, int id)
        {
            OrderItem o = new OrderItem()
            {
                Id = id,
                Order = orderItem.Order,
                Product = orderItem.Product,
                Quantity = orderItem.Quantity
            };

            await _orderItemService.Update(o);
            return NoContent();
        }

        // /orderItem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemService.Delete(id);
            return NoContent();
        }
    }
}
