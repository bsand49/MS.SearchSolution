namespace MS.SearchSolution.BE.Models
{
    public class PersonSearchResponseContainer
    {
        public IEnumerable<Person> Persons { get; set; }

        #region Constructors
        public PersonSearchResponseContainer()
        {
            Persons = new List<Person>();
        }
        public PersonSearchResponseContainer(IEnumerable<Person> persons)
        {
            Persons = persons;
        }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            if (obj is not PersonSearchResponseContainer)
                return false;

            return Equals((PersonSearchResponseContainer)obj);
        }
        private bool Equals(PersonSearchResponseContainer container)
        {
            return Persons.SequenceEqual(container.Persons);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Persons);
        }
        #endregion
    }
}
