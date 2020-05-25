using dotNetCoreTest;
using dotNetCoreTest.Controllers;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace BankApiTests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PositiveTest()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();

            var parameters = new BankApiTestRequest
            {
                XAuthHeader = "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L",
                ContentTypeHeader = "application/json",
                BankAccount = "GB09HAOE91311808002317",
            };

            var bankApiTestClass = new TestController(logger);
            var response = bankApiTestClass.BankApiTestRequest(parameters);

            Assert.AreEqual(true, response.Result.IsValid);
        }

        [Test]
        public void NegativeTest()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TestController>();

            var parameters = new BankApiTestRequest
            {
                ContentTypeHeader = "application/json",
                BankAccount = "GB09HAOE91311808002317",
            };

            var bankApiTestClass = new TestController(logger);
            var response = bankApiTestClass.BankApiTestRequest(parameters);

            Assert.AreEqual("Authorization has been denied for this request.", response.Result.Message);
        }
    }
}