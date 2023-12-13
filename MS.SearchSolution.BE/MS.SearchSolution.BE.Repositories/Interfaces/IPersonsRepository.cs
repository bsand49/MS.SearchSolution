using MS.SearchSolution.BE.Models;

namespace MS.SearchSolution.BE.Repositories.Interfaces
{
    public interface IPersonsRepository
    {
        public Task<IEnumerable<Person>> GetPersonsAsync();
    }
}
