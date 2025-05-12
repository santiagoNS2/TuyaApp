using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Application.DTOs
{
    /// <summary>
    /// Datos necesarios para crear una nueva orden asociada a un cliente.
    /// </summary>
    public class CreateOrderDto
    {
        /// <summary>
        /// ID del cliente que realizará la orden.
        /// </summary>
        public Guid CustomerId { get; set; }
    }
}
