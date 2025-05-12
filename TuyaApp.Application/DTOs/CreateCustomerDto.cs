using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Application.DTOs
{
    /// <summary>
    /// Datos necesarios para registrar un nuevo cliente en el sistema.
    /// </summary>
    public class CreateCustomerDto
    {
        /// <summary>
        /// Nombre completo del cliente.
         /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Dirección de correo electrónico del cliente.
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Cédula de ciudadanía o documento de identificación único del cliente.
        /// </summary>
        public string CC { get; set; } = default!;
    }
}

