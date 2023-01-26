using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order:BaseEntity
    {
        public string? OrderNo { get; set; } = Guid.NewGuid().ToString().Substring(0, 6);
        public int? ShoppingCartId { get; set; }
        //[ForeignKey("ShoppingCartId")]
        //public ShoppingCart? ShoppingCart { get; set; }
        public string? IdentityUserId { get; set; }
        //[ForeignKey("UserId")]
        //public RegisterModel? User { get; set; }
        public double? TotalPrice { get; set; }

    }
}
