using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderAPI.DAL;
using OrderAPI.Infrastructure;
using OrderAPI.Model;
using OrderAPI.Model.ViewModel;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly APIDbContext _dbContext;

        public OrdersController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET api/orders/
        public async Task<string> Get()
        {
            try
            {
                string result = string.Empty;

                List<Order> orders = await _dbContext
                    .Orders.Include(x => x.Customer)
                        .ToListAsync();

                if (orders != null)
                {
                    result = JsonConvert.SerializeObject(new { info = "success", message = "ok", errors = "", data = orders });
                }
                else
                    result = JsonConvert.SerializeObject(new { info = "error", message = "Order is not found", errors = "", data = "" });
               
                return result;
            }
            catch (Exception exp)
            {
                return JsonConvert.SerializeObject(new { info = "error", message = exp.Message, errors = exp.Message, data = "" });
            }
        }
        
        //GET api/orders/get/{id}
        [HttpGet("get/{id}")]
        public async Task<string> Get(string id)
        {
            try
            {
                string result = string.Empty;

                if (id != null)
                {
                    List<Order> orders = new List<Order>
                    {
                        await _dbContext
                            .Orders.Include(x => x.Customer)
                                .FirstOrDefaultAsync(x => x.Id == id)
                    };

                    if (orders != null)
                    {
                        result = JsonConvert.SerializeObject(new { info = "success", message = "ok", errors="", data = orders });
                    }
                    else
                        result = JsonConvert.SerializeObject(new { info = "error", message = "Order is not found", errors = "", data = "" });
                }
                else
                    result = JsonConvert.SerializeObject(new { info = "error", message = "Id is not found", errors = "", data = "" });

                return result;
            }
            catch (Exception exp)
            {
                return JsonConvert.SerializeObject(new { info = "error", message = exp.Message, errors = "", data = "" });
            }
        }

        // POST api/post
        [HttpPost("post")]
        public async Task<string> Post([FromBody] OrderCustomerViewModel orderCustomer)
        {
            try
            {
                string result = string.Empty;

                if (orderCustomer != null)
                { 
                    List<Tuple<string,string>> errors = Validate.
                                        ValidateObject(orderCustomer);

                    if (errors.Count == 0)
                    {
                        Customer customer = new Customer()
                        {
                            Name = orderCustomer.CustomerName,
                            Email = orderCustomer.CustomerEmail,
                            AddressMovingFrom = orderCustomer.CustomerAddressMovingFrom,
                            AddressMovingTo = orderCustomer.CustomerAddressMovingTo,
                            Note = orderCustomer.CustomerNote,
                            OrderDate = orderCustomer.CustomerOrderDate,
                            PhoneNumber = orderCustomer.CustomerPhoneNumber
                        };

                        Order order = new Order()
                        {
                            Name = orderCustomer.OrderName,
                            OrderType = orderCustomer.OrderType,
                            Customer = customer
                        };

                        await _dbContext.Orders.AddAsync(order);

                        await _dbContext.SaveChangesAsync();

                        result = JsonConvert.SerializeObject(new { info = "success", message = "ok", errors, data="" });
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(new { info = "error", message = "Information is not found", errors , data = "" });
                    }
                }
                return result;
            }
            catch (Exception exp)
            {
                return JsonConvert.SerializeObject(new { info = "error", message = "Information is not found", errors= exp.Message, data = "" });
            }
        }

        //Delete api/orders/delete/id
        [HttpDelete("delete/{id}")]
        public async Task<string> Delete(string id)
        {
            try
            {
                string result = string.Empty;

                if (id != null)
                {
                    Order order = _dbContext
                        .Orders.Include(x=>x.Customer)
                                .FirstOrDefault(x => x.Id == id);

                    if (order != null)
                    {
                        _dbContext.Orders.Remove(order);

                        await _dbContext.SaveChangesAsync();

                        result = JsonConvert.SerializeObject(new { info = "success", message = "ok", errors = "", data = "" });
                    }
                    else
                        result = JsonConvert.SerializeObject(new { info = "error", message = "Order is not found", errors ="", data = "" });
                }
                else
                    result = JsonConvert.SerializeObject(new { info = "error", message = "Id is not found", errors = "", data = "" });

                return result;
            }
            catch (Exception exp)
            {
                return JsonConvert.SerializeObject(new { info = "error", message = exp.Message, errors = "", data = "" });
            }
        }

        // Edit api/edit
        [HttpPut("edit")]
        public async Task<string> Edit([FromBody] OrderCustomerViewModel orderCustomer)
        {
            try
            {
                string result = string.Empty;

                if (orderCustomer != null)
                {
                    List<Tuple<string, string>> errors = Validate.
                                        ValidateObject(orderCustomer);

                    if (errors.Count ==0 )
                    {
                        Order order = _dbContext.Orders.Include(x=>x.Customer)
                            .FirstOrDefault(x => x.Id == orderCustomer.OrderId);

                        if(order!=null)
                        {
                            order.Name = orderCustomer.OrderName;
                            order.OrderType = orderCustomer.OrderType;

                            if (order.Customer!=null)
                            {
                                order.Customer.Name = orderCustomer.CustomerName;
                                order.Customer.Note = orderCustomer.CustomerNote;
                                order.Customer.OrderDate = orderCustomer.CustomerOrderDate;
                                order.Customer.PhoneNumber = orderCustomer.CustomerPhoneNumber;
                                order.Customer.AddressMovingFrom = orderCustomer.CustomerAddressMovingFrom;
                                order.Customer.AddressMovingTo = orderCustomer.CustomerAddressMovingTo;
                                order.Customer.Email = orderCustomer.CustomerEmail;
                            }

                            _dbContext.Update(order);

                            await _dbContext.SaveChangesAsync();

                            result = JsonConvert.SerializeObject(new { info = "success", message = "ok", errors, data = "" });
                        }
                        else
                            result = JsonConvert.SerializeObject(new { info = "error", message = "Order is not found", errors, data = "" });
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(new { info = "error", message = "Information is not found", errors, data = "" });
                    }
                }
                return result;
            }
            catch (Exception exp)
            {
                return JsonConvert.SerializeObject(new { info = "error", message = "Information is not found", errors = exp.Message, data = "" });
            }
        }
    }
}
