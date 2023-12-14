using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MS.SearchSolution.BE.API.Controllers;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Services.Interfaces;

namespace MS.SearchSolution.BE.Tests.ControllerTests
{
    [TestFixture(TestOf = typeof(SearchController))]
    [Category("SearchControllerTests")]
    [CancelAfter(250)]
    public class SearchControllerTests
    {
        private Mock<ILogger<SearchController>> _logger;
        private Mock<ISearchService> _searchService;
        private SearchController _searchController;

        private readonly PersonSearchResponseContainer _personSearchResponseContainer = new(new List<Person>()
        {
            new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
            new(2, "Katey", "Gaines", "kgaines1@bbb.org", GenderEnum.Female),
            new(3, "Ardelle", "Soames", "asoames2@google.it", GenderEnum.Female),
            new(4, "Kalila", "Tennant", "ktennant3@phpbb.com", GenderEnum.Female),
            new(5, "Veda", "Emma", "vemma4@nature.com", GenderEnum.Female),
            new(6, "Pauli", "Isacke", "pisacke5@is.gd", GenderEnum.Female),
            new(7, "Belita", "Ruoff", "bruoff6@wiley.com", GenderEnum.Female),
            new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
            new(9, "Jasen", "Jiroudek", "jjiroudek8@google.it", GenderEnum.Polygender),
            new(10, "Gusty", "Tuxill", "gtuxill9@illinois.edu", GenderEnum.Female)
        });

        private readonly PersonSearchResponseContainer _emptyPersonSearchResponseContainer = new();

        private const string _unexpectedErrorMessage = "Unexpected error occured.";

        #region Setup & Teardown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new();
        }

        [SetUp]
        public void SetUp()
        {
            _searchService = new();
            _searchController = new SearchController(_logger.Object, _searchService.Object);
        }
        #endregion

        #region GetPersonsBySearchTermAsync Tests
        [Test]
        public async Task SearchController_GetPersonsBySearchTermAsync_StringSearchTerm_Returns200Ok()
        {
            // Setup
            var searchTerm = "James";
            _searchService.Setup(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s.Equals(searchTerm)), CancellationToken.None)).ReturnsAsync(_personSearchResponseContainer);

            // Function Call
            var result = await _searchController.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
                Assert.That(((OkObjectResult)result).StatusCode, Is.Not.Null);
                Assert.That(((OkObjectResult)result).Value, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(((OkObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(((OkObjectResult)result).Value, Is.EqualTo(_personSearchResponseContainer));
            });

            // Verify Mocks
            _searchService.Verify(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s.Equals(searchTerm)), CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchController_GetPersonsBySearchTermAsync_NullSearchTerm_Returns500InternalServerError()
        {
            // NOTE - This test does not represent a real world scenario, due to searchTerm in the endpoint being a required field.
            // The endpoint validation would catch this, and return a 400 Bad Request

            // Setup
            string? searchTerm = null;
            _searchService.Setup(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s.Equals(searchTerm)), CancellationToken.None)).ReturnsAsync(_emptyPersonSearchResponseContainer);

            // Function Call
#pragma warning disable CS8604 // Possible null reference argument.
            var result = await _searchController.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);
#pragma warning restore CS8604 // Possible null reference argument.

            // Assertions
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf(typeof(ObjectResult)));
                Assert.That(((ObjectResult)result).StatusCode, Is.Not.Null);
                Assert.That(((ObjectResult)result).Value, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                
                Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
                Assert.That(((ObjectResult)result).Value, Is.TypeOf(typeof(ErrorResponse)));
                Assert.That(((ErrorResponse)((ObjectResult)result).Value).Code, Is.EqualTo(StatusCodes.Status500InternalServerError));
                Assert.That(((ErrorResponse)((ObjectResult)result).Value).Message, Is.EqualTo(_unexpectedErrorMessage));
        });

            // Verify Mocks
            _searchService.Verify(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s == null), CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchController_GetPersonsBySearchTermAsync_EmptySearchTerm_Returns200Ok()
        {
            // NOTE - This test does not represent a real world scenario, due to searchTerm in the endpoint being a required field.
            // The endpoint validation would catch this, and return a 400 Bad Request

            // Setup
            string searchTerm = string.Empty;
            _searchService.Setup(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s.Equals(searchTerm)), CancellationToken.None)).ReturnsAsync(_emptyPersonSearchResponseContainer);

            // Function Call
            var result = await _searchController.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
                Assert.That(((OkObjectResult)result).StatusCode, Is.Not.Null);
                Assert.That(((OkObjectResult)result).Value, Is.Not.Null);
            });
            Assert.Multiple(() =>
            {
                Assert.That(((OkObjectResult)result).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(((OkObjectResult)result).Value, Is.EqualTo(_emptyPersonSearchResponseContainer));
            });

            // Verify Mocks
            _searchService.Verify(mock => mock.GetPersonsBySearchTermAsync(It.Is<string>(s => s.Equals(searchTerm)), CancellationToken.None), Times.Once);
        }
        #endregion
    }
}
