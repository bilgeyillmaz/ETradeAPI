using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class WalletDetailDto:BaseDto
    {
        public string? UserName { get; set; }
        public double? WalletBalance { get; set; }
    }
}
