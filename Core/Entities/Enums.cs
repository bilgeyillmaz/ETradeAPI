using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
    public static class Enums
    {
        public enum UserRoles
        {
            [Display(Name = "Admin")] Admin = 1,
            [Display(Name = "Member")] Member = 2
        }
    }
}
