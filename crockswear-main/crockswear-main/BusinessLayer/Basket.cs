using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Basket
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public Shoe Shoe { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(0,999999)]
        public decimal Price { get; set; }
        [Range(0, 999999)]
        [Required]
        public decimal Shoeprice { get; set; }
        public OrderStatus Status { get; set; }
        public Bill Bill { get; set; }


        public Basket    ()
        {

        }



        public Basket(int id,User user, Shoe shoe, int quantity, decimal price, decimal shoeprice, OrderStatus status)
        {
            this.Id= id;
            this.Shoe= shoe;
            this.Quantity= quantity;
            this.Price= price;
            this.Shoeprice= shoeprice;
            this.Status= status;
               

        }
















    }
}
