using MS.SearchSolution.BE.Models;

namespace MS.SearchSolution.BE.Tests.ModelsTests
{
    [TestFixture(TestOf = typeof(Person))]
    [Category("PersonTests")]
    [CancelAfter(250)]
    public class PersonTests
    {
        private const int _personId = 1;
        private const string _personFirstName = "Test";
        private const string _personLastName = "Person";
        private const string _personEmail = "test.person@email.com";
        private const GenderEnum _personGender = GenderEnum.Female;

        #region Constructors Tests
        [Test]
        public void Person_DefaultConstructor_ReturnsDefaultPersonObject()
        {
            // Setup
            var expected = new Person()
            {
                Id = 0,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Gender = GenderEnum.Female
            };

            // Function Call
            var result = new Person();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Person_ParametersConstructor_ReturnsPersonObjectWithSpecifiedPropertyValues()
        {
            // Setup
            var expected = new Person()
            {
                Id = _personId,
                FirstName = _personFirstName,
                LastName = _personLastName,
                Email = _personEmail,
                Gender = _personGender
            };

            // Function Call
            var result = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }
        #endregion

        #region Overrides Tests
        [Test]
        public void Person_Equals_EqualObjects_ReturnsTrue()
        {
            // Setup
            var person1 = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);
            var person2 = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);
            var person3 = new Person();
            var person4 = new Person(0, string.Empty, string.Empty, string.Empty, GenderEnum.Female);

            // Function Call
            var result1 = person1.Equals(person2);
            var result2 = person3.Equals(person4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.True);
                Assert.That(result2, Is.True);
            });
        }

        [Test]
        public void Person_Equals_NotEqualObjects_ReturnsTrue()
        {
            // Setup
            var person1 = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);
            var person2 = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);
            var person3 = new Person();
            var person4 = new Person(0, string.Empty, string.Empty, string.Empty, GenderEnum.Female);

            // Function Call
            var result1 = person1.Equals(person3);
            var result2 = person1.Equals(person4);
            var result3 = person2.Equals(person3);
            var result4 = person2.Equals(person4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.False);
                Assert.That(result2, Is.False);
                Assert.That(result3, Is.False);
                Assert.That(result4, Is.False);
            });
        }

        [Test]
        public void Person_GetHashCode_EqualObjects_ReturnsSameHashCodes()
        {
            // Setup
            var expected = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender).GetHashCode();
            var person = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender);

            // Function Call
            var result = person.GetHashCode();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Person_GetHashCode_NotEqualObjects_ReturnsDifferentHashCodes()
        {
            // Setup
            var expected = new Person(_personId, _personFirstName, _personLastName, _personEmail, _personGender).GetHashCode();
            
            var person1 = new Person(0, _personFirstName, _personLastName, _personEmail, _personGender);
            var person2 = new Person(_personId, string.Empty, _personLastName, _personEmail, _personGender);
            var person3 = new Person(_personId, _personFirstName, string.Empty, _personEmail, _personGender);
            var person4 = new Person(_personId, _personFirstName, _personLastName, string.Empty, _personGender);
            var person5 = new Person(_personId, _personFirstName, _personLastName, _personEmail, GenderEnum.Male);
            var person6 = new Person(0, string.Empty, string.Empty, string.Empty, GenderEnum.Male);

            // Function Call
            var result1 = person1.GetHashCode();
            var result2 = person2.GetHashCode();
            var result3 = person3.GetHashCode();
            var result4 = person4.GetHashCode();
            var result5 = person5.GetHashCode();
            var result6 = person6.GetHashCode();

            // Assertions
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.EqualTo(expected));
                Assert.That(result1, Is.Not.EqualTo(result2));
                Assert.That(result1, Is.Not.EqualTo(result3));
                Assert.That(result1, Is.Not.EqualTo(result4));
                Assert.That(result1, Is.Not.EqualTo(result5));
                Assert.That(result1, Is.Not.EqualTo(result6));
                
                Assert.That(result2, Is.Not.EqualTo(expected));
                Assert.That(result2, Is.Not.EqualTo(result3));
                Assert.That(result2, Is.Not.EqualTo(result4));
                Assert.That(result2, Is.Not.EqualTo(result5));
                Assert.That(result2, Is.Not.EqualTo(result6));
                
                Assert.That(result3, Is.Not.EqualTo(expected));
                Assert.That(result3, Is.Not.EqualTo(result4));
                Assert.That(result3, Is.Not.EqualTo(result5));
                Assert.That(result3, Is.Not.EqualTo(result6));

                Assert.That(result4, Is.Not.EqualTo(expected));
                Assert.That(result4, Is.Not.EqualTo(result5));
                Assert.That(result4, Is.Not.EqualTo(result6));

                Assert.That(result5, Is.Not.EqualTo(expected));
                Assert.That(result5, Is.Not.EqualTo(result6));

                Assert.That(result6, Is.Not.EqualTo(expected));
            });
        }
        #endregion
    }
}
