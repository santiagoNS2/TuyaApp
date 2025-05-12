using System.Net;
using System.Net.Http.Json;
using TuyaApp.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.EntityFrameworkCore;


namespace TuyaApp.Tests.Integration
{
    public class CustomerEndpointsTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CustomerEndpointsTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomers_ReturnsEmptyList_Initially()
        {
            var response = await _client.GetAsync("/api/customer");
            response.EnsureSuccessStatusCode();

            var customers = await response.Content.ReadFromJsonAsync<List<CustomerResponseDto>>();
            Assert.NotNull(customers);
            Assert.Empty(customers!);
        }

        [Fact]
        public async Task CreateCustomer_ThenGetCustomers_ReturnsCreatedCustomer()
        {
            var newCustomer = new CreateCustomerDto
            {
                Name = "Test User",
                Email = "test@example.com",
                CC = "987654321"
            };

            var post = await _client.PostAsJsonAsync("/api/customer", newCustomer);
            post.EnsureSuccessStatusCode();

            var get = await _client.GetAsync("/api/customer");
            var customers = await get.Content.ReadFromJsonAsync<List<CustomerResponseDto>>();

            Assert.NotNull(customers);
            Assert.Single(customers!);
            Assert.Equal("Test User", customers[0].Name);
        }
    }
}
