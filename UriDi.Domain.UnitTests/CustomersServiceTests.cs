using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UriDi.Domain.Services;
using UriDi.Infrastructure.HttpClients;

namespace UriDi.Domain.UnitTests
{
    public class CustomersServiceTests
    {
        [Test]
        public async Task Should_Return_Odd_Customers()
        {
            var customerHttpClient = new Mock<ICustomersHttpClient>(MockBehavior.Strict);
            var fakeCustomers = new List<string> { "CUSTOMER1", "CUSTOMER2", "CUSTOMER3", "CUSTOMER4", "CUSTOMER5" };
            customerHttpClient.Setup(x => x.GetCustomersAsync()).ReturnsAsync(fakeCustomers);
            var customersService = new CustomersService(customerHttpClient.Object);

            var customers = await customersService.GetOddCustomersAsync();

            var expectedCustomers = new List<string>
            {
                "CUSTOMER1",
                "CUSTOMER3",
                "CUSTOMER5"
            };
            
            customers.Should().BeEquivalentTo(expectedCustomers);
        }
    }
}
