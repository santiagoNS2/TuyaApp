using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyaApp.Domain.Entities;
using TuyaApp.Domain.Interfaces;

namespace TuyaApp.Application.Services
{
    public class OrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new ArgumentException("Cliente no encontrado.");

            var order = new Order(customer.Id);
            await _orderRepository.AddAsync(order);

            return order;
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
                throw new ArgumentException("Orden no encontrada.");

            existingOrder.Cancel();
            await _orderRepository.UpdateAsync(existingOrder);
        }
    }
}
