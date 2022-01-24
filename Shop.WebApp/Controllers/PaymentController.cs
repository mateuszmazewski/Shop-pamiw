using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shop.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.WebApp.Controllers
{
    [Authorize()]
    public class PaymentController : Controller
    {
        public IConfiguration Configuration;

        public PaymentController(IConfiguration configuration)
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
            List<PaymentVM> paymentsList = new List<PaymentVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    paymentsList = JsonConvert.DeserializeObject<List<PaymentVM>>(apiResponse);
                }
            }

            return View(paymentsList);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            PaymentVM payment = new PaymentVM();
            OrderVM actualOrder;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<PaymentVM>(apiResponse);
                }

                _restpath = GetHostUrl().Content + "order";
                using (var response = await httpClient.GetAsync($"{_restpath}/{payment.OrderId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    actualOrder = JsonConvert.DeserializeObject<OrderVM>(apiResponse);
                }
            }

            ViewBag.OrdersList = new List<OrderVM>() { actualOrder }; // Only one order - actual (user can't change it)
            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaymentVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            PaymentVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync($"{_restpath}/{vm.Id}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<PaymentVM>(apiResponse);
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
            string _restpath = GetHostUrl().Content + "order";
            List<OrderVM> ordersList = new List<OrderVM>();

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync(_restpath))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ordersList = JsonConvert.DeserializeObject<List<OrderVM>>(apiResponse);
                }
            }

            ViewBag.OrdersList = ordersList.Where(x => x.PaymentId.Equals(0)).ToList(); // Only orders that do not have a payment assigned yet
            return View(new PaymentVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            PaymentVM result;

            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(vm);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(_restpath, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<PaymentVM>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(PaymentVM vm)
        {
            string _restpath = GetHostUrl().Content + ControllerName();
            PaymentVM payment;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{vm.Id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<PaymentVM>(apiResponse);
                }
            }

            return View(payment);
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
            PaymentVM payment;

            using (var httpClient = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } }))
            {
                using (var response = await httpClient.GetAsync($"{_restpath}/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    payment = JsonConvert.DeserializeObject<PaymentVM>(apiResponse);
                }
            }

            return View(payment);
        }
    }
}
