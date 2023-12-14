using MS.SearchSolution.BE.Models;

namespace MS.SearchSolution.BE.Services.Interfaces
{
    public interface ISearchService
    {
        Task<PersonSearchResponseContainer> GetPersonsBySearchTermAsync(string searchTerm, CancellationToken ct);
    }
}
