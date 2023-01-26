using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Wallet:BaseEntity
    {
        public string? IdentityUserId { get; set; }
        //[ForeignKey("UserId")]
        //public RegisterModel? User { get; set; }
        public double? Balance { get; set; }

    }
}
