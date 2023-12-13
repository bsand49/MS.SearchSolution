using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MS.SearchSolution.BE.Models
{
    [DataContract]
    public class Person
    {

        [DataMember(Name = "id")]
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [DataMember(Name = "first_name")]
        [JsonProperty(Required = Required.Always)]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        [JsonProperty(Required = Required.Always)]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        [JsonProperty(Required = Required.Always)]
        public string Email { get; set; }

        [DataMember(Name = "gender")]
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
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
