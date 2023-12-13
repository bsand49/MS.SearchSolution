using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MS.SearchSolution.BE.Models
{
    [DataContract]
    public class Person
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "gender")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GenderEnum Gender { get; set; }

        #region Constructors
        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
        public Person(int id, string firstName, string lastName, string email, GenderEnum gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
        }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            if (obj is not Person)
                return false;

            return Equals((Person)obj);
        }
        private bool Equals(Person person)
        {
            return
                Id.Equals(person.Id) &&
                FirstName.Equals(person.FirstName) &&
                LastName.Equals(person.LastName) &&
                Email.Equals(person.Email) &&
                Gender.Equals(person.Gender);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, Gender);
        }
        #endregion
    }
}
