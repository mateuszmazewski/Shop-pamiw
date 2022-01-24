using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shop.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebApp.Controllers
{
    [Authorize()]
    public class OrderItemController : Controller
    {
        public IConfiguration Configuration;

        public OrderItemController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ContentResult GetHostUrl()
        {
            var result = Configuration["RestApiUrl:HostUrl"];
            return Content(result);
        }

        private string ControllerName()
        {
            string cn = ControllerContext.RouteData.Values["controller"].ToString();
            return cn;
        }

        [HttpGet]
        [Route("OrderItem/Create/{orderId:int}")] // Add orderItem for specific order
        public async Task<IActionResult> Create(int orderId)
        {
            string _restpath = GetHostUrl().Content + "product";
            List<ProductVM> productsList = new List<ProductVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productsList = JsonConvert.DeserializeObject<List<ProductVM>>(apiResponse);
                }
            }

            var orderItemVM = new OrderItemVM()
            {
                OrderId = orderId
            };
            ViewBag.ProductsList = productsList;
            return View(orderItemVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItemVM vm)
        {
            string _restpath;
            OrderItemVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    _restpath = GetHostUrl().Content + $"Product/{vm.ProductId}";
                    ProductVM product;
                    using (var response = await httpClient.GetAsync(_restpath))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<ProductVM>(apiResponse);
                    }

                    if (product.UnitOfMeasurement == Shop.Core.Domain.UnitOfMeasurement.Pieces)
                    {
                        if (vm.Quantity % 1 != 0) // Not integer
                        {
                            ViewBag.Product = product;
                            return View("WrongQuantity", vm);
                        }
                    }

                    if (product.Stock < vm.Quantity)
                    {
                        ViewBag.Product = product;
                        return View("NotEnoughProducts", vm);
                    }

                    product.Stock -= vm.Quantity;

                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    _restpath = GetHostUrl().Content + ControllerName();

                    using (var response = await httpClient.PostAsync(_restpath, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                    }

                    _restpath = GetHostUrl().Content + $"Product/{vm.ProductId}";
                    jsonString = System.Text.Json.JsonSerializer.Serialize(product);
                    content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(_restpath, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return Redirect($"/Order/Details/{vm.Id}"); // Go back to order details view
        }

        public async Task<IActionResult> Delete(int id)
        {
            string _restpath = GetHostUrl().Content + ControllerName();

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    using (var response = await httpClient.DeleteAsync($"{_restpath}/{id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return Redirect(Request.Headers["Referer"].ToString()); // Redirect to previous page
        }
    }
}
