using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public List<Basket> Orders { get; set; }
        

        public User() {
            Orders = new List<Basket>();
        }

        public User(int id, string name, string email, string password)
        {
            this.Id= id;
            this.Name= name;
            this.Email= email;
            this.Password= password;

            Orders = new List<Basket>();

        }
    }
}
