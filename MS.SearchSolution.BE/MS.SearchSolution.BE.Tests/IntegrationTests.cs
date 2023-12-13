using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using MS.SearchSolution.BE.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MS.SearchSolution.BE.Tests
{
    [TestFixture(TestOf = typeof(Program))]
    [Category("IntegrationTests")]
    [CancelAfter(250)]
    public class IntegrationTests
    {
        private HttpClient _httpClient;

        private const string _healthCheckPath = "/api/healthcheck";
        private const string _searchPersonsPath = "/api/search/persons";

        private const string _searchTermFieldRequiredErrorMessage = "The searchTerm field is required.";

        #region Setup & Teardown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Ensure we're running integration tests in the development environment
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
        }
        #endregion

        #region /api/healthcheck Tests
        [Test]
        public async Task GetHealthCheck_GET_Returns200Ok()
        {
            // Function Call
            var response = await _httpClient.GetAsync(_healthCheckPath);

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetHealthCheck_POST_Returns405NotAllowed()
        {
            // Setup
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // Function Call
            var response = await _httpClient.PostAsync(_healthCheckPath, content);

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
        }

        [Test]
        public async Task GetHealthCheck_PUT_Returns405NotAllowed()
        {
            // Setup
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // Function Call
            var response = await _httpClient.PutAsync(_healthCheckPath, content);

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
        }
        #endregion

        #region /api/search Tests
        [Test]
        public async Task GetPersonsBySearchTerm_GET_James_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "James";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_Jam_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "jam";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(8, "James", "Kubu", "hkubu7@craigslist.org", GenderEnum.Male),
                new(11, "James", "Pfeffer", "bpfeffera@amazon.com", GenderEnum.Male),
                new(14, "Chalmers", "Longfut", "clongfujam@wp.com", GenderEnum.Male)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_KateySoltan_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "Katey%20Soltan";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_DotCom_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = ".com";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
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

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_O_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "O";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
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

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_8_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "8";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
                new(9, "Jasen", "Jiroudek", "jjiroudek8@google.it", GenderEnum.Polygender)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_WhitespaceS_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "%20S";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(3, "Ardelle", "Soames", "asoames2@google.it", GenderEnum.Female),
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_YWhitespace_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "Y%20";
            var expectedResponseObject = new PersonSearchResponseContainer(new List<Person>()
            {
                new(1, "Antony", "Fitt", "afitt0@a8.net", GenderEnum.Male),
                new(2, "Katey", "Gaines", "kgaines1@bbb.org", GenderEnum.Female),
                new(10, "Gusty", "Tuxill", "gtuxill9@illinois.edu", GenderEnum.Female),
                new(18, "Katey", "Soltan", "ksoltanh@simplemachines.org", GenderEnum.Female)
            });

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_JasmineDuncan_Returns200OkWithExpectedResults()
        {
            // Setup
            var searchTerm = "Jasmine%20Duncan";
            var expectedResponseObject = new PersonSearchResponseContainer();

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<PersonSearchResponseContainer>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_NullSearchTerm_Returns400BadRequest()
        {
            // Setup
            var expectedResponseObject = new ErrorResponse(StatusCodes.Status400BadRequest, _searchTermFieldRequiredErrorMessage);

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_EmptySearchTerm_Returns400BadRequest()
        {
            // Setup
            var searchTerm = string.Empty;
            var expectedResponseObject = new ErrorResponse(StatusCodes.Status400BadRequest, _searchTermFieldRequiredErrorMessage);

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_GET_WhitespaceSearchTerm_Returns400BadRequest()
        {
            // Setup
            var searchTerm = "%20%20";
            var expectedResponseObject = new ErrorResponse(StatusCodes.Status400BadRequest, _searchTermFieldRequiredErrorMessage);

            // Function Call
            var response = await _httpClient.GetAsync($"{_searchPersonsPath}?searchTerm={searchTerm}");

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(responseString, Is.Not.Null.Or.Empty);

            var responseObject = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
            Assert.That(responseObject, Is.Not.Null);
            Assert.That(responseObject, Is.EqualTo(expectedResponseObject));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_POST_Returns405NotAllowed()
        {
            // Setup
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // Function Call
            var response = await _httpClient.PostAsync(_searchPersonsPath, content);

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
        }

        [Test]
        public async Task GetPersonsBySearchTerm_PUT_Returns405NotAllowed()
        {
            // Setup
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // Function Call
            var response = await _httpClient.PutAsync(_searchPersonsPath, content);

            // Assertions
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
        }
        #endregion
    }
}
