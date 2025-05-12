using Castle.Core.Resource;
using Moq;
using TuyaApp.Application.Services;
using TuyaApp.Domain.Entities;
using TuyaApp.Domain.Interfaces;
using Xunit;

namespace TuyaApp.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder_WhenCustomerExists()
        {
            
            var customerId = Guid.NewGuid();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
          
   
            var customer = new Customer("Santiago", "correo@correo.com", "123456");

            typeof(Customer)
            .GetProperty("Id")!
             .SetValue(customer, customerId);

            mockCustomerRepo.Setup(r => r.GetByIdAsync(customerId))
                            .ReturnsAsync(customer);

            var orderService = new OrderService(mockCustomerRepo.Object, mockOrderRepo.Object);

            
            var result = await orderService.CreateOrderAsync(customerId);

         
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            mockOrderRepo.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowException_WhenCustomerNotFound()
        {
        
            var customerId = Guid.NewGuid();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();

            mockCustomerRepo.Setup(r => r.GetByIdAsync(customerId))
                            .ReturnsAsync((Customer?)null);

            var orderService = new OrderService(mockCustomerRepo.Object, mockOrderRepo.Object);

          
            await Assert.ThrowsAsync<ArgumentException>(() => orderService.CreateOrderAsync(customerId));
        }
        [Fact]
        public async Task CancelOrderAsync_ShouldCancelOrder_WhenOrderExists()
        {
          
            var orderId = Guid.NewGuid();
            var existingOrder = new Order(Guid.NewGuid());

            typeof(Order).GetProperty("Id")!.SetValue(existingOrder, orderId);

            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetByIdAsync(orderId))
                         .ReturnsAsync(existingOrder);

            var service = new OrderService(mockCustomerRepo.Object, mockOrderRepo.Object);

          
            await service.CancelOrderAsync(existingOrder.Id);

           
            Assert.True(existingOrder.IsCanceled);
            mockOrderRepo.Verify(r => r.UpdateAsync(It.Is<Order>(o => o.IsCanceled)), Times.Once);
        }

        [Fact]
        public async Task CancelOrderAsync_ShouldThrowException_WhenOrderNotFound()
        {
           
            var orderId = Guid.NewGuid();
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetByIdAsync(orderId))
                         .ReturnsAsync((Order?)null);

            var service = new OrderService(mockCustomerRepo.Object, mockOrderRepo.Object);

           
            await Assert.ThrowsAsync<ArgumentException>(() => service.CancelOrderAsync(orderId));
        }

        [Fact]
        public async Task CancelOrderAsync_ShouldThrowException_WhenOrderAlreadyCanceled()
        {
            
            var orderId = Guid.NewGuid();
            var existingOrder = new Order(Guid.NewGuid());
            typeof(Order).GetProperty("Id")!.SetValue(existingOrder, orderId);

            
            typeof(Order).GetProperty("IsCanceled")!
                         .SetValue(existingOrder, true);

            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(r => r.GetByIdAsync(orderId))
                         .ReturnsAsync(existingOrder);

            var service = new OrderService(mockCustomerRepo.Object, mockOrderRepo.Object);

           
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CancelOrderAsync(existingOrder.Id));
        }



    }
}
