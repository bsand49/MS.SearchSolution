using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MS.SearchSolution.BE.API.Controllers;

namespace MS.SearchSolution.BE.Tests.ControllerTests
{
    [TestFixture(TestOf = typeof(HealthCheckController))]
    [Category("HealthCheckControllerTests")]
    [CancelAfter(250)]
    public class HealthCheckControllerTests
    {
        private Mock<ILogger<HealthCheckController>> _logger;
        private HealthCheckController _healthCheckController;

        #region Setup & Teardown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new();
        }

        [SetUp]
        public void SetUp()
        {
            _healthCheckController = new HealthCheckController(_logger.Object);
        }
        #endregion

        #region GetHealthCheck Tests
        [Test]
        public void HealthCheckController_GetHealthCheck_Returns200Ok()
        {
            // Function Call
            var result = _healthCheckController.GetHealthCheck();

            // Assertions
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf(typeof(OkResult)));
                Assert.That(((OkResult)result).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            });
        }
        #endregion
    }
}
