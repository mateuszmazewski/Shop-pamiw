using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Infrastructure.Commands;
using Shop.Infrastructure.Services;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // /payment
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var p = await _paymentService.BrowseAll();
            return Json(p);
        }

        // /payment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var p = await _paymentService.Get(id);
            return Json(p);

        }

        // /payment/filter?orderId=4
        [HttpGet("filter")]
        public async Task<IActionResult> GetPaymentByFilter(int orderId)
        {
            var p = await _paymentService.GetByFilter(orderId);
            return Json(p);
        }

        // /payment
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] CreatePayment payment)
        {
            Payment p = new Payment()
            {
                Order = payment.Order,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                PaymentMethod = payment.PaymentMethod
            };

            await _paymentService.Add(p);
            return CreatedAtRoute(null, new { id = p.Id }, p);
        }

        // /payment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPayment([FromBody] UpdatePayment payment, int id)
        {
            Payment p = new Payment()
            {
                Id = id,
                Order = payment.Order,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                PaymentMethod = payment.PaymentMethod
            };

            await _paymentService.Update(p);
            return NoContent();
        }

        // /payment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _paymentService.Delete(id);
            return NoContent();
        }
    }
}
