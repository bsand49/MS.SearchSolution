using Microsoft.Extensions.Logging;
using Moq;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Repositories.Interfaces;
using MS.SearchSolution.BE.Services;

namespace MS.SearchSolution.BE.Tests.ServicesTests
{
    [TestFixture(TestOf = typeof(SearchService))]
    [Category("SearchServiceTests")]
    [CancelAfter(250)]
    public class SearchServiceTests
    {
        private Mock<ILogger<SearchService>> _logger;
        private Mock<IPersonsRepository> _personsRepository;
        private SearchService _searchService;

        private readonly IEnumerable<Person> _emptyPersonsList = new List<Person>();
        private readonly IEnumerable<Person> _personsList = new List<Person>()
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
            new(10, "Gusty", "Tuxill", "gtuxill9@illinois.edu", GenderEnum.Female),
            new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male),
            new(12, "Mignonne", "Whitewood", "mwhitewoodb@godaddy.com", GenderEnum.Female),
            new(13, "Moselle", "Gaize", "mgaizec@tumblr.com", GenderEnum.Female),
            new(14, "Chalmers", "Longfut", "clongfujam@wp.com", GenderEnum.Male),
            new(15, "Linnell", "Jumont", "ljumonte@storify.com", GenderEnum.Female),
            new(16, "Viole", "Eaglen", "veaglenf@nytimes.com", GenderEnum.Female),
            new(17, "Sileas", "Tarr", "starrg@telegraph.co.uk", GenderEnum.Female),
            new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female),
            new(19, "Gar", "Motion", "gmotioni@shop-pro.jp", GenderEnum.Male),
            new(20, "Kameko", "Vanes", "kvanesj@discuz.net", GenderEnum.Female)
        };

        private readonly PersonSearchResponseContainer _emptyPersonSearchResponseContainer = new();

        private readonly Exception _genericExcpetion = new("Something went wrong.");

        #region Setup & Teardown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new();
        }

        [SetUp]
        public void Setup()
        {
            _personsRepository = new();
            _searchService = new(_logger.Object, _personsRepository.Object);
        }
        #endregion

        #region GetPersonsBySearchTermAsync Tests
        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_James_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "James";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_Jam_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "jam";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male),
                new(14, "Chalmers", "Longfut", "clongfujam@wp.com", GenderEnum.Male)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_KateySoltan_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "Katey Soltan";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_DotCom_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = ".com";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(4, "Kalila", "Tennant", "ktennant3@phpbb.com", GenderEnum.Female),
                new(5, "Veda", "Emma", "vemma4@nature.com", GenderEnum.Female),
                new(7, "Belita", "Ruoff", "bruoff6@wiley.com", GenderEnum.Female),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male),
                new(12, "Mignonne", "Whitewood", "mwhitewoodb@godaddy.com", GenderEnum.Female),
                new(13, "Moselle", "Gaize", "mgaizec@tumblr.com", GenderEnum.Female),
                new(14, "Chalmers", "Longfut", "clongfujam@wp.com", GenderEnum.Male),
                new(15, "Linnell", "Jumont", "ljumonte@storify.com", GenderEnum.Female),
                new(16, "Viole", "Eaglen", "veaglenf@nytimes.com", GenderEnum.Female)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_O_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "O";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
                new(2, "Katey", "Gaines", "kgaines1@bbb.org", GenderEnum.Female),
                new(3, "Ardelle", "Soames", "asoames2@google.it", GenderEnum.Female),
                new(4, "Kalila", "Tennant", "ktennant3@phpbb.com", GenderEnum.Female),
                new(5, "Veda", "Emma", "vemma4@nature.com", GenderEnum.Female),
                new(7, "Belita", "Ruoff", "bruoff6@wiley.com", GenderEnum.Female),
                new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
                new(9, "Jasen", "Jiroudek", "jjiroudek8@google.it", GenderEnum.Polygender),
                new(10, "Gusty", "Tuxill", "gtuxill9@illinois.edu", GenderEnum.Female),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male),
                new(12, "Mignonne", "Whitewood", "mwhitewoodb@godaddy.com", GenderEnum.Female),
                new(13, "Moselle", "Gaize", "mgaizec@tumblr.com", GenderEnum.Female),
                new(14, "Chalmers", "Longfut", "clongfujam@wp.com", GenderEnum.Male),
                new(15, "Linnell", "Jumont", "ljumonte@storify.com", GenderEnum.Female),
                new(16, "Viole", "Eaglen", "veaglenf@nytimes.com", GenderEnum.Female),
                new(17, "Sileas", "Tarr", "starrg@telegraph.co.uk", GenderEnum.Female),
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female),
                new(19, "Gar", "Motion", "gmotioni@shop-pro.jp", GenderEnum.Male),
                new(20, "Kameko", "Vanes", "kvanesj@discuz.net", GenderEnum.Female)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }
        
        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_8_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "8";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
                new(9, "Jasen", "Jiroudek", "jjiroudek8@google.it", GenderEnum.Polygender)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_WhitespaceS_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = " S";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(3, "Ardelle", "Soames", "asoames2@google.it", GenderEnum.Female),
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_YWhitespace_DataFoundAndFilterFindsResults_ReturnsPersonSearchResponseContainerWithIEnumerable()
        {
            // Setup
            var searchTerm = "y ";
            var expectedResult = new PersonSearchResponseContainer(new List<Person>()
            {
                new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
                new(2, "Katey", "Gaines", "kgaines1@bbb.org", GenderEnum.Female),
                new(10, "Gusty", "Tuxill", "gtuxill9@illinois.edu", GenderEnum.Female),
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(expectedResult));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_JasmineDuncan_DataFoundAndFilterFindsNoResults_ReturnsPersonSearchResponseContainerWithEmptyIEnumerable()
        {
            // Setup
            var searchTerm = "Jasmine Duncan";
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_personsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(_emptyPersonSearchResponseContainer));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task SearchService_GetPersonsBySearchTermAsync_DataNotFound_ReturnsPersonSearchResponseContainerWithEmptyIEnumerable()
        {
            // Setup
            var searchTerm = "";
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ReturnsAsync(_emptyPersonsList);

            // Function Call
            var result = await _searchService.GetPersonsBySearchTermAsync(searchTerm, CancellationToken.None);

            // Assertions
            Assert.That(result, Is.EqualTo(_emptyPersonSearchResponseContainer));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void SearchService_GetPersonsBySearchTermAsync_ExceptionThrownRetrievingData_ThrowsException()
        {
            // Setup
            _personsRepository.Setup(mock => mock.GetPersonsAsync(CancellationToken.None)).ThrowsAsync(_genericExcpetion);

            // Function Call
            var result = Assert.ThrowsAsync<Exception>(async() => await _searchService.GetPersonsBySearchTermAsync(string.Empty, CancellationToken.None));

            // Assertions
            Assert.That(result.Message, Is.EqualTo(_genericExcpetion.Message));

            // Verify Mocks
            _personsRepository.Verify(mock => mock.GetPersonsAsync(CancellationToken.None), Times.Once);
        }
        #endregion
    }
}
