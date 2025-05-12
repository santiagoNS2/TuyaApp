using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TuyaApp.Application.DTOs
{
    /// <summary>
    /// Representa los datos públicos de un cliente devueltos por la API.
    /// </summary>
    public class CustomerResponseDto
    {
        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nombre completo del cliente.
        /// </summary>
        public string Name { get; set; } = default!;
        /// <summary>
        /// Correo electrónico del cliente.
        /// </summary>
        public string Email { get; set; } = default!;
        /// <summary>
        /// Número de cédula del cliente.
        /// </summary>
        public string CC { get; set; } = default!;
    }
}

