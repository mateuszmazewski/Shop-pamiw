using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Infrastructure.Commands;
using Shop.Infrastructure.Services;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // /order
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var o = await _orderService.BrowseAll();
            return Json(o);
        }

        // /order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var o = await _orderService.Get(id);
            return Json(o);

        }

        // /order/filter?customerId=3
        [HttpGet("filter")]
        public async Task<IActionResult> GetOrderByFilter(int customerId)
        {
            var o = await _orderService.BrowseAllByFilter(customerId);
            return Json(o);
        }

        // /order
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrder order)
        {
            Order o = new Order()
            {
                Customer = order.Customer,
                CustomerId = order.CustomerId,
                Payment = order.Payment,
                PaymentId = order.PaymentId
            };

            await _orderService.Add(o);
            return CreatedAtRoute(null, new { id = o.Id }, o);
        }

        // /order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrder([FromBody] UpdateOrder order, int id)
        {
            Order o = new Order()
            {
                Id = id,
                Customer = order.Customer,
                CustomerId = order.CustomerId,
                Payment = order.Payment,
                PaymentId = order.PaymentId,
                CreatedAt = order.CreatedAt
            };

            await _orderService.Update(o);
            return NoContent();
        }

        // /order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.Delete(id);
            return NoContent();
        }
    }
}
