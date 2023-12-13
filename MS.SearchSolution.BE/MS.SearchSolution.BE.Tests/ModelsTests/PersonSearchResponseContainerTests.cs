using MS.SearchSolution.BE.Models;
using NUnit.Framework.Internal;

namespace MS.SearchSolution.BE.Tests.ModelsTests
{
    [TestFixture(TestOf = typeof(PersonSearchResponseContainer))]
    [Category("PersonSearchResponseContainerTests")]
    [CancelAfter(250)]
    public class PersonSearchResponseContainerTests
    {
        private readonly Person _person1 = new();
        private readonly Person _person2 = new(1, "Test", "Person", "test.person@email.com", GenderEnum.Female);

        #region Constructors Tests
        [Test]
        public void PersonSearchResponseContainer_DefaultConstructor_ReturnsDefaultPersonObject()
        {
            // Setup
            var expected = new PersonSearchResponseContainer()
            {
                Persons = new List<Person>()
            };

            // Function Call
            var result = new PersonSearchResponseContainer();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void PersonSearchResponseContainer_ParametersConstructor_ReturnPersonSearchResponseContainerObjectWithSpecifiedPropertyValues()
        {
            // Setup
            var personsList = new List<Person>() { _person1, _person2 };

            var expected = new PersonSearchResponseContainer()
            {
                Persons = personsList
            };

            // Function Call
            var result = new PersonSearchResponseContainer(personsList);

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        #endregion

        #region Overrides Tests
        [Test]
        public void PersonSearchResponseContainer_Equals_EqualObjects_ReturnsExpectedTruth()
        {
            // Setup
            var personsList1 = new List<Person>() { _person1, _person2 };
            var personsList2 = new List<Person>();

            var container1 = new PersonSearchResponseContainer(personsList1);
            var container2 = new PersonSearchResponseContainer(personsList1);
            var container3 = new PersonSearchResponseContainer();
            var container4 = new PersonSearchResponseContainer(personsList2);

            // Function Call
            var result1 = container1.Equals(container2);
            var result2 = container3.Equals(container4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.True);
                Assert.That(result2, Is.True);
            });
        }

        [Test]
        public void PersonSearchResponseContainer_Equals_NotEqualObjects_ReturnsExpectedTruth()
        {
            // Setup
            var personsList1 = new List<Person>() { _person1, _person2 };
            var personsList2 = new List<Person>();

            var container1 = new PersonSearchResponseContainer(personsList1);
            var container2 = new PersonSearchResponseContainer(personsList1);
            var container3 = new PersonSearchResponseContainer();
            var container4 = new PersonSearchResponseContainer(personsList2);

            // Function Call
            var result1 = container1.Equals(container3);
            var result2 = container1.Equals(container4);
            var result3 = container2.Equals(container3);
            var result4 = container2.Equals(container4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.False);
                Assert.That(result2, Is.False);
                Assert.That(result3, Is.False);
                Assert.That(result4, Is.False);
            });
        }

        [Test]
        public void PersonSearchResponseContainer_GetHashCode_EqualObjects_ReturnsSameHashCodes()
        {
            // Setup
            var personsList = new List<Person>() { _person1, _person2 };

            var expected = new PersonSearchResponseContainer(personsList).GetHashCode();
            var container = new PersonSearchResponseContainer(personsList);

            // Function Call
            var result = container.GetHashCode();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void PersonSearchResponseContainer_GetHashCode_NotEqualObjects_ReturnsDifferentHashCodes()
        {
            // Setup
            var personsList1 = new List<Person>();
            var personsList2 = new List<Person>() { _person1 };
            var personsList3 = new List<Person>() { _person2 };

            var expected = new PersonSearchResponseContainer(new List<Person>() { _person1, _person2 }).GetHashCode();

            var container1 = new PersonSearchResponseContainer(personsList1);
            var container2 = new PersonSearchResponseContainer(personsList2);
            var container3 = new PersonSearchResponseContainer(personsList3);

            // Function Call
            var result1 = container1.GetHashCode();
            var result2 = container2.GetHashCode();
            var result3 = container3.GetHashCode();

            // Assertions
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.EqualTo(expected));
                Assert.That(result1, Is.Not.EqualTo(result2));
                Assert.That(result1, Is.Not.EqualTo(result3));

                Assert.That(result2, Is.Not.EqualTo(expected));
                Assert.That(result2, Is.Not.EqualTo(result3));

                Assert.That(result3, Is.Not.EqualTo(expected));
            });
        }
        #endregion
    }
}
