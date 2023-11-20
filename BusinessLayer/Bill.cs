using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Bill
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string BankCard { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<Order> Orders { get; set; }
            


        public Bill() { 
            Orders = new List<Order>();
        }

        public Bill(int id, User user, string address, string bankcard, decimal price, List<Order> orders) {
            this.Id = id;
            this.User = user;
            this.Address = address;
            this.BankCard = bankcard;
            this.Price = price;
            Orders = orders;

        }


    }
}
