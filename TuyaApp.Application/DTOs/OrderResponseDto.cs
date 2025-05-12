using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Application.DTOs
{
    //Recordar que esta es opcional
    //para prboar con el front

    /// <summary>
    /// Representa la información básica de una orden registrada.
    /// </summary>
    public class OrderResponseDto
    {
        /// <summary>
        /// Identificador único de la orden.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ID del cliente asociado a la orden.
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Fecha en que fue creada la orden.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Indica si la orden fue cancelada.
        /// </summary>
        public bool IsCanceled { get; set; }
    }
}
