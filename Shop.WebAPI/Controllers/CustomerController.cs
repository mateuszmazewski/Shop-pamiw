using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Infrastructure.Commands;
using Shop.Infrastructure.Services;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // /customer
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var c = await _customerService.BrowseAll();
            return Json(c);
        }

        // /customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var c = await _customerService.Get(id);
            return Json(c);

        }

        // /customer/filter?name=jan&surname=kowalski
        [HttpGet("filter")]
        public async Task<IActionResult> GetCustomer(string name, string surname)
        {
            var c = await _customerService.BrowseAllByFilter(name, surname);
            return Json(c);
        }

        // /customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomer customer)
        {
            Customer c = new Customer()
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth
            };

            await _customerService.Add(c);
            return CreatedAtRoute(null, new { id = c.Id }, c);
        }

        // /customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomer([FromBody] UpdateCustomer customer, int id)
        {
            Customer c = new Customer()
            {
                Id = id,
                Name = customer.Name,
                Surname = customer.Surname,
                Address = customer.Address,
                DateOfBirth = customer.DateOfBirth
            };

            await _customerService.Update(c);
            return NoContent();
        }

        // /customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.Delete(id);
            return NoContent();
        }
    }
}
