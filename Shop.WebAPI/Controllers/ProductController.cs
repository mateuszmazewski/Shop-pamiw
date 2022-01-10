using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Infrastructure.Commands;
using Shop.Infrastructure.Services;
using System.Threading.Tasks;

namespace Shop.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // /product
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var p = await _productService.BrowseAll();
            return Json(p);
        }

        // /product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var c = await _productService.Get(id);
            return Json(c);

        }

        // /product/filter?name=dysk
        [HttpGet("filter")]
        public async Task<IActionResult> GetProduct(string name)
        {
            var p = await _productService.BrowseAllByFilter(name);
            return Json(p);
        }

        // /product
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProduct product)
        {
            Product p = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                UnitOfMeasurement = product.UnitOfMeasurement
            };

            await _productService.Add(p);
            return CreatedAtRoute(null, new { id = p.Id }, p);
        }

        // /product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct([FromBody] UpdateProduct product, int id)
        {
            Product p = new Product()
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                UnitOfMeasurement = product.UnitOfMeasurement
            };

            await _productService.Update(p);
            return NoContent();
        }

        // /product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Delete(id);
            return NoContent();
        }
    }
}
