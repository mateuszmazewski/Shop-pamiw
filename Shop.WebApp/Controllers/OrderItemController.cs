using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shop.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zawodnicy.WebApp.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            List<OrderItemVM> orderItemsList = new List<OrderItemVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItemsList = JsonConvert.DeserializeObject<List<OrderItemVM>>(apiResponse);
                }
            }

            return View(orderItemsList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderItemVM orderItem = new OrderItemVM();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItem = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                }
            }

            return View(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderItemVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderItemVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{_restpath}/{vm.Id}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Index));
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
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderItemVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(_restpath, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(OrderItemVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderItemVM orderItem;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{vm.Id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItem = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                }
            }

            return View(orderItem);
        }

        [HttpPost]
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderItemVM orderItem;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItem = JsonConvert.DeserializeObject<OrderItemVM>(apiResponse);
                }
            }

            return View(orderItem);
        }
    }
}
