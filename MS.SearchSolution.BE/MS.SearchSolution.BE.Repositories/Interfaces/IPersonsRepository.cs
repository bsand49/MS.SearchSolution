using MS.SearchSolution.BE.Models;

namespace MS.SearchSolution.BE.Repositories.Interfaces
{
    public interface IPersonsRepository
    {
        Task<IEnumerable<Person>> GetPersonsAsync();
    }
}
