using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Infrastructure.Service;
using OrderSystem.Models;
using OrderSystem.Models.Main;
using OrderSystem.Models.ViewModel;

namespace OrderSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                OrderApiResponse<Order> orderApiResponse = await ServiceProvider
                    .GetDataAsync<OrderApiResponse<Order>>("https://localhost:44369/api/orders");

                return View(orderApiResponse.Data);
            }
            catch (Exception exp)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (id != null)
                {
                    OrderApiResponse<Order> orderApiResponse = await ServiceProvider
                        .GetDataAsync<OrderApiResponse<Order>>("https://localhost:44369/api/orders/get/" + id);

                    if (orderApiResponse.Message == "ok")
                    {
                        OrderCustomerViewModel orderCustomerViewModel = new OrderCustomerViewModel()
                        {
                            OrderId = orderApiResponse.Data.FirstOrDefault().Id,
                            OrderName = orderApiResponse.Data.FirstOrDefault().Name,
                            OrderType = orderApiResponse.Data.FirstOrDefault().OrderType,
                            CustomerName = orderApiResponse.Data.FirstOrDefault().Customer.Name,
                            CustomerEmail = orderApiResponse.Data.FirstOrDefault().Customer.Email,
                            CustomerPhoneNumber = orderApiResponse.Data.FirstOrDefault().Customer.PhoneNumber,
                            CustomerAddressMovingFrom = orderApiResponse.Data.FirstOrDefault().Customer.AddressMovingFrom,
                            CustomerAddressMovingTo = orderApiResponse.Data.FirstOrDefault().Customer.AddressMovingTo,
                            CustomerNote = orderApiResponse.Data.FirstOrDefault().Customer.Note,
                            CustomerOrderDate = orderApiResponse.Data.FirstOrDefault().Customer.OrderDate
                        };

                        return View(orderCustomerViewModel);
                    }

                    ViewBag.Error = "Order is not found";
                }
            }
            catch (Exception exp)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderCustomerViewModel orderCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please, Enter correct information");

                    return View(orderCustomer);
                }
                else
                {
                    OrderApiResponse<Order> orderApiResponse = await ServiceProvider
                        .PutDataAsync<OrderApiResponse<Order>>(orderCustomer, "https://localhost:44369/api/orders/edit/");

                    if (orderApiResponse?.Message == "ok")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Order is not edited");

                        return View(orderCustomer);
                    }
                }
            }
            catch (Exception exp)
            {
                return NotFound();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    OrderApiResponse<Order> orderApiResponse = await ServiceProvider
                        .DeleteDataAsync<OrderApiResponse<Order>>("https://localhost:44369/api/orders/delete/" + id);

                    if (orderApiResponse?.Message != "ok")
                        ViewBag.Error = "Order is not deleted";
                }
                return RedirectToAction("Index");
            }
            catch (Exception exp)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCustomerViewModel orderCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Please,Enter correct information");

                    return View();
                }
                else
                {
                    OrderApiResponse<Order> orderApiResponse = await ServiceProvider
                        .PostDataAsync<OrderApiResponse<Order>>(orderCustomer, "https://localhost:44369/api/orders/post/");

                    if (orderApiResponse.Message == "ok")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Order is not created");

                        return View();
                    }
                }
            }
            catch (Exception exp)
            {
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
