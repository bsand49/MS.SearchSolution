using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Repositories.Interfaces;
using Newtonsoft.Json;

namespace MS.SearchSolution.BE.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly ILogger<PersonsRepository> _logger;
        private readonly IConfiguration _config;

        private string PersonsDataFilePath => _config.GetSection("PersonsDataFilePath").Value ?? throw new Exception("PersonsDataFilePath not set in config.");

        #region Constructors
        public PersonsRepository(ILogger<PersonsRepository> logger, IConfiguration config) => (_logger, _config) = (logger, config);
        #endregion

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            try
            {
                var personsListJson = await File.ReadAllTextAsync(PersonsDataFilePath);
                _logger.LogInformation("Read json {personsListJson} from file {file}.", personsListJson, PersonsDataFilePath);

                return JsonConvert.DeserializeObject<IEnumerable<Person>>(personsListJson) ?? Enumerable.Empty<Person>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PersonsRepository.GetPersonsAsync() threw an exception.");
                throw;
            }
        }
    }
}
