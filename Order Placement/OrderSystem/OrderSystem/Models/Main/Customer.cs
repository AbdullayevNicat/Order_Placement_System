using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Models.Main
{
    public class Customer
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string AddressMovingFrom { get; set; }
        [Required]
        public string AddressMovingTo { get; set; }
        public string Note { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
    }
}
