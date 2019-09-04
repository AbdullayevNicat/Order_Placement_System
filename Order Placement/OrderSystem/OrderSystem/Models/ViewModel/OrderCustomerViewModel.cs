using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Models.ViewModel
{
    public class OrderCustomerViewModel
    {
        public string OrderId { get; set; }

        [Required]
        public string OrderName { get; set; }

        public string OrderType { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerAddressMovingFrom { get; set; }
        [Required]
        public string CustomerAddressMovingTo { get; set; }
        public string CustomerNote { get; set; }
        [Required]
        public DateTime CustomerOrderDate { get; set; }
    }
}
