using Microsoft.AspNetCore.Mvc;
using TuyaApp.Application.DTOs;
using TuyaApp.Domain.Entities;
using TuyaApp.Domain.Interfaces;

namespace TuyaApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            var customer = new Customer(dto.Name, dto.Email, dto.CC);
            await _customerRepository.AddAsync(customer);

            var response = new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                CC = customer.CC
            };

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, response);
        }
        /// <summary>
        /// Devuelve todos los clientes registrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
        {
            var customers = await _customerRepository.GetAllAsync();

            var result = customers.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                CC = c.CC
            });

            return Ok(result);
        }
        /// <summary>
        /// Busca un cliente por su ID único
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerResponseDto>> GetById(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                CC = customer.CC
            });
        }
        /// <summary>
        /// Busca un cliente por su CC para no buscarlo por ID
        /// </summary>
        [HttpGet("cc/{cc}")]
        public async Task<ActionResult<CustomerResponseDto>> GetByCC(string cc)
        {
            var customer = await _customerRepository.GetByCCAsync(cc);
            if (customer == null)
                return NotFound();

            return Ok(new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                CC = customer.CC
            });
        }
        /// <summary>
        /// Actualiza los datos de un cliente existente
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCustomerDto dto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.SetName(dto.Name);
            existingCustomer.SetEmail(dto.Email);
            existingCustomer.SetCC(dto.CC);

            await _customerRepository.UpdateAsync(existingCustomer);
            return NoContent();
        }
        /// <summary>
        /// Elimina un cliente por su ID
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
                return NotFound();

            await _customerRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}

