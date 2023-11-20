using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Shoe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(15,48)]
        public int Size { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Too long brand name")]
        public string Brand { get; set; }

        [Required]

        [MaxLength(100, ErrorMessage = "Too long model name")]
        public string Model { get; set; }


        [Required]
        [Range(5,5000)]
        public decimal Price { get; set; }

        [Required]
        public string Color { get; set; }

        [MaxLength(5000, ErrorMessage = "Too long description")]
        public string Description { get; set; }

        [Required]
        public Manager Manager { get; set; }


        public List<Order> Orders { get; set; }





        public Shoe ()
        {
            Orders= new List<Order> ();
        }
        public Shoe(int id, int size, string brand, string model, decimal price, string color, string description, Manager manager)
        {
                this.Id = id;
                this.Brand = brand;
                this.Model = model;
                this.Size = size;
                this.Price = price;  
                this.Color = color; 
                this.Description = description; 
            this.Manager = manager;
            Orders = new List<Order>();



        }
    }
}