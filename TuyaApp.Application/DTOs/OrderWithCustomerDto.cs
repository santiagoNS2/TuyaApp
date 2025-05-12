using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Application.DTOs
{
    public class OrderWithCustomerDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCanceled { get; set; }

        public CustomerResponseDto Customer { get; set; } = default!;
    }
}

