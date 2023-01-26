using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShoppingCart:BaseEntity
    {
        public string? IdentityUserId { get; set; }
        //[ForeignKey("UserId")]
        //public RegisterModel? User { get; set; }
        public double? Price { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>(); 
    }
}
