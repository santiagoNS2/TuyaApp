using Microsoft.AspNetCore.Mvc;
using TuyaApp.Application.DTOs;
using TuyaApp.Application.Services;
using TuyaApp.Domain.Entities;
using TuyaApp.Domain.Interfaces;
using TuyaApp.Infrastructure.Persistence;

namespace TuyaApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepository;


        public OrderController(OrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Crea una nueva orden asociada a un cliente existente.
        /// </summary>
        /// <remarks>
        /// Requiere el ID del cliente en el cuerpo de la solicitud.
        /// </remarks>
        /// <response code="200">Orden creada exitosamente</response>
        /// <response code="404">Cliente no encontrado</response>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(dto.CustomerId);

                var response = new OrderResponseDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    CreatedAt = order.CreatedAt,
                    IsCanceled = order.IsCanceled
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una lista de todas las órdenes creadas junto con sus clientes.
        /// </summary>
        /// <response code="200">Lista de órdenes devuelta correctamente</response>

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();

            var result = orders.Select(order => new OrderWithCustomerDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                IsCanceled = order.IsCanceled,
                Customer = new CustomerResponseDto
                {
                    Id = order.Customer!.Id,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    CC = order.Customer.CC
                }
            });

            return Ok(result);
        }

        /// <summary>
        /// Cancela una orden existente si aún no ha sido cancelada.
        /// </summary>
        /// <param name="id">ID de la orden a cancelar</param>
        /// <response code="204">Orden cancelada correctamente</response>
        /// <response code="400">La orden ya está cancelada</response>
        /// <response code="404">Orden no encontrada</response>

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var order = new Order(id); 
            try
            {
                await _orderService.CancelOrderAsync(id);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles de una orden específica, incluyendo los datos del cliente.
        /// </summary>
        /// <param name="id">ID de la orden</param>
        /// <response code="200">Orden encontrada</response>
        /// <response code="404">Orden no encontrada</response>

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var dto = new OrderWithCustomerDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                IsCanceled = order.IsCanceled,
                Customer = new CustomerResponseDto
                {
                    Id = order.Customer!.Id,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    CC = order.Customer.CC
                }
            };

            return Ok(dto);
        }


    }
}
