using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace MS.SearchSolution.BE.Tests.RepositoriesTests
{
    [TestOf(typeof(PersonsRepository))]
    [Category("PersonsRepositoryTests")]
    [CancelAfter(250)]
    public class PersonsRepositoryTests
    {
        private Mock<ILogger<PersonsRepository>> _logger;
        private IConfiguration _config;
        private PersonsRepository _personsRepository;

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

        private const string _configJsonFileDoesNotExist = "{\"PersonsDataFilePath\": \"data/not_a_real_file.json\"}";
        private const string _configJsonFileEmpty = "{\"PersonsDataFilePath\": \"data/empty.json\"}";
        private const string _configJsonFileJsonArrayEmpty = "{\"PersonsDataFilePath\": \"data/json_array_empty.json\"}";
        private const string _configJsonFileJsonArrayNotPersons = "{\"PersonsDataFilePath\": \"data/json_array_not_persons_empty.json\"}";
        private const string _configJsonFileIncorrectFormat = "{\"PersonsDataFilePath\": \"data/not_json.txt\"}";

        private const string _filePathNotInConfigExceptionMessage = "PersonsDataFilePath not set in config.";
        private const string _fileNotFoundExceptionMessage = "Could not find file";
        private const string _requiredPropertyMissingExceptionMessage = "Required property 'id' not found in JSON";
        private const string _jsonSerializationExceptionMessage = "Error converting value";

        #region Setup & Teardown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new();
        }

        [SetUp]
        public void SetUp()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
            _personsRepository = new PersonsRepository(_logger.Object, _config);
        }
        #endregion

        #region GetPersonsAsync Tests
        [Test]
        public async Task PersonsRepository_GetPersonsAsync_FileExistsAndContainsValidData_ReturnsIEnumerable()
        {
            // Function Call
            var result = await _personsRepository.GetPersonsAsync();

            // Assertions
            Assert.That(result, Is.EqualTo(_personsList));
        }

        [Test]
        public void PersonsRepository_GetPersonsAsync_FilePathNotInConfig_ThrowsException()
        {
            // Setup
            var config = new ConfigurationBuilder().Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = Assert.ThrowsAsync<Exception>(async () => await _personsRepository.GetPersonsAsync());

            // Assertions
            Assert.That(result.Message, Is.EqualTo(_filePathNotInConfigExceptionMessage));
        }

        [Test]
        public void PersonsRepository_GetPersonsAsync_FileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Setup
            var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(_configJsonFileDoesNotExist))).Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = Assert.ThrowsAsync<FileNotFoundException>(async () => await _personsRepository.GetPersonsAsync());

            // Assertions
            Assert.That(result.Message, Does.StartWith(_fileNotFoundExceptionMessage));
        }

        [Test]
        public async Task PersonsRepository_GetPersonsAsync_FileExistsButIsEmpty_ReturnsEmptyIEnumerable()
        {
            // Setup
            var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(_configJsonFileEmpty))).Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = await _personsRepository.GetPersonsAsync();

            // Assertions
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task PersonsRepository_GetPersonsAsync_FileExistsButJsonArrayIsEmpty_ReturnsEmptyIEnumerable()
        {
            // Setup
            var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(_configJsonFileJsonArrayEmpty))).Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = await _personsRepository.GetPersonsAsync();

            // Assertions
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void PersonsRepository_GetPersonsAsync_FileExistsButJsonArrayContainsNonPersonObjects_ThrowsJsonSerializationException()
        {
            // Setup
            var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(_configJsonFileJsonArrayNotPersons))).Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = Assert.ThrowsAsync<JsonSerializationException>(async () => await _personsRepository.GetPersonsAsync());

            // Assertions
            Assert.That(result.Message, Does.StartWith(_requiredPropertyMissingExceptionMessage));
        }

        [Test]
        public void PersonsRepository_GetPersonsAsync_FileExistsButDataNotInJsonFormat_ThrowsJsonSerializationException()
        {
            // Setup
            var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(_configJsonFileIncorrectFormat))).Build();
            _personsRepository = new(_logger.Object, config);

            // Function Call
            var result = Assert.ThrowsAsync<JsonSerializationException>(async() => await _personsRepository.GetPersonsAsync());

            // Assertions
            Assert.That(result.Message, Does.StartWith(_jsonSerializationExceptionMessage));
        }
        #endregion
    }
}
