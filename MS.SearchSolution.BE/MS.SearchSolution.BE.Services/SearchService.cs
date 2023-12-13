using Microsoft.Extensions.Logging;
using MS.SearchSolution.BE.Models;
using MS.SearchSolution.BE.Repositories.Interfaces;
using MS.SearchSolution.BE.Services.Interfaces;

namespace MS.SearchSolution.BE.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> _logger;
        private readonly IPersonsRepository _personsRepository;

        #region Constructors
        public SearchService(ILogger<SearchService> logger, IPersonsRepository personsRepository) => (_logger, _personsRepository) = (logger, personsRepository);
        #endregion

        public async Task<PersonSearchResponseContainer> GetPersonsBySearchTermAsync(string searchTerm)
        {
            try
            {
                // Get all persons from dataset
                var allPersons = await _personsRepository.GetPersonsAsync();
                _logger.LogInformation("Found {count} persons.", allPersons.Count());

                // If no persons found, return immediately, as there's nothing to filter
                if (!allPersons.Any())
                    return new();

                // Filter all persons using search term on
                // - FirstName contains search term
                // - LastName contains search term
                // - Email contains search term
                // - FirstName LastName contains search term
                // where casing of strings are ignored
                var filteredPersons = allPersons
                    .Where(person =>
                        person.FirstName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                        person.LastName.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                        person.Email.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                        string.Concat(person.FirstName, " ", person.LastName).Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));

                _logger.LogInformation("Filtered to {count} persons.", filteredPersons.Count());

                return new(filteredPersons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SearchService.GetPersonsBySearchTermAsync() threw an exception.");
                throw;
            }
        }
    }
}
