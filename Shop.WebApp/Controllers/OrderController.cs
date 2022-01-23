using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using X.PagedList;
using Shop.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zawodnicy.WebApp.Controllers
{
    [Authorize()]
    public class OrderController : Controller
    {
        public IConfiguration Configuration;

        public OrderController(IConfiguration configuration)
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
            List<OrderVM> ordersList = new List<OrderVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ordersList = JsonConvert.DeserializeObject<List<OrderVM>>(apiResponse);
                }
            }

            return View(ordersList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderVM order = new OrderVM();
            List<CustomerVM> customersList = new List<CustomerVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                }

                _restpath = GetHostUrl().Content + "customer";
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<CustomerVM>>(apiResponse);
                }
            }

            ViewBag.CustomersList = customersList;
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{_restpath}/{vm.Id}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            string _restpath = GetHostUrl().Content + "customer";
            List<CustomerVM> customersList = new List<CustomerVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customersList = JsonConvert.DeserializeObject<List<CustomerVM>>(apiResponse);
                }
            }

            ViewBag.CustomersList = customersList;
            return View(new OrderVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(_restpath, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(OrderVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderVM order;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{vm.Id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                }
            }

            return View(order);
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

        public async Task<IActionResult> Details(int id, string currentFilter, string searchString, int? page)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            OrderVM order;
            List<OrderItemVM> orderItemsList;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    order = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                }

                _restpath = GetHostUrl().Content + $"OrderItem/filter?orderId={id}";
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    orderItemsList = JsonConvert.DeserializeObject<List<OrderItemVM>>(apiResponse);
                }
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            double total = 0;
            foreach (OrderItemVM orderItem in orderItemsList)
            {
                total += Math.Round(orderItem.Product.Price * orderItem.Quantity, 2);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                orderItemsList = orderItemsList.Where(x => x.Product.Name.Contains(searchString)).ToList();
            }

            ViewBag.Total = Math.Round(total, 2);
            ViewBag.OrderItemsPagedList = orderItemsList.ToPagedList(pageNumber, pageSize);
            return View(order);
        }
    }
}
