using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MS.SearchSolution.BE.API.Controllers
{
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        #region Constructors
        public HealthCheckController(ILogger<HealthCheckController> logger) => _logger = logger;
        #endregion

        [HttpGet]
        [SwaggerOperation(Summary = "Gets health check", OperationId = "GetHealthCheck")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful Operation")]
        public IActionResult GetHealthCheck()
        {
            return Ok();
        }
    }
}
