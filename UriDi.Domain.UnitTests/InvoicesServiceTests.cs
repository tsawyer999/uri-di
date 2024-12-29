using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UriDi.Domain.Services;
using UriDi.Infrastructure.HttpClients;

namespace UriDi.Domain.UnitTests
{
    public class InvoicesServiceTests
    {
        [Test]
        public async Task Should_Return_Even_Invoices()
        {
            var invoicesHttpClient = new Mock<IInvoicesHttpClient>(MockBehavior.Strict);
            var fakeInvoices = new List<string> { "INVOICE1", "INVOICE2", "INVOICE3", "INVOICE4", "INVOICE5" };
            invoicesHttpClient.Setup(x => x.GetInvoicesAsync()).ReturnsAsync(fakeInvoices);
            var invoicesService = new InvoicesService(invoicesHttpClient.Object);

            var invoices = await invoicesService.GetEvenInvoicesAsync();

            var expectedCustomers = new List<string>
            {
                "INVOICE2",
                "INVOICE4"
            };
            
            invoices.Should().BeEquivalentTo(expectedCustomers);
        }
    }
}
