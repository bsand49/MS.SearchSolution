using Microsoft.AspNetCore.Mvc;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MS.SearchSolution.BE.API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchService _searchService;

        #region Constructors
        public SearchController(ILogger<SearchController> logger, ISearchService searchService) => (_logger, _searchService) = (logger, searchService);
        #endregion

        [HttpGet("persons")]
        [SwaggerOperation(Summary = "Gets persons filtered using search term", OperationId = "GetPersonsBySearchTerm")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful Operation", typeof(PersonSearchResponseContainer))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error", typeof(ErrorResponse))]
        public async Task<IActionResult> GetPersonsBySearchTermAsync(
            [Required][FromQuery] string searchTerm,
            CancellationToken ct)
        {
            try
            {
                var responseContainer = await _searchService.GetPersonsBySearchTermAsync(searchTerm, ct);

                return Ok(responseContainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SearchController.GetPersonsBySearchTermAsync() threw an exception.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, "Unexpected error occured."));
            }
        }
    }
}
