using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ShoppingCartDetailDto:BaseDto
    {
        public string? CustomerName { get; set; }
        public double? ShoppingCartPrice { get; set; }
        public List<Product>? ShoppingCartProducts { get; set; }
        public int? CountOfProducts { get; set; }
        public string? IdentityUserId { get; set; }
    }
}
