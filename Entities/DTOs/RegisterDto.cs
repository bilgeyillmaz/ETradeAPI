using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RegisterDto:IDto
    {
        [Required(ErrorMessage = "Name and Surname is required.")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email address is required.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Password address is required.")]
        public string? Password { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
    }
}
