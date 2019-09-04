using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Models.Main
{
    public class Order
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string OrderType { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
