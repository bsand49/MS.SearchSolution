using MS.SearchSolution.BE.Models;

namespace MS.SearchSolution.BE.Tests.ModelsTests
{
    [TestFixture(TestOf = typeof(ErrorResponse))]
    [Category("ErrorResponseTests")]
    [CancelAfter(250)]
    public class ErrorResponseTests
    {
        private const int _code = 123;
        private const string _message = "A message.";

        #region Constructors Tests
        [Test]
        public void ErrorResponse_DefaultConstructor_ReturnsErrorResponseObject()
        {
            // Setup
            var expected = new ErrorResponse()
            {
                Code = 0,
                Message = string.Empty
            };

            // Function Call
            var result = new ErrorResponse();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ErrorResponse_ParametersConstructor_ReturnsErrorResponseObjectWithSpecifiedPropertyValues()
        {
            // Setup
            var expected = new ErrorResponse()
            {
                Code = _code,
                Message = _message
            };

            // Function Call
            var result = new ErrorResponse(_code, _message);

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }
        #endregion

        #region Overrides Tests
        [Test]
        public void ErrorResponse_Equals_EqualObjects_ReturnsTrue()
        {
            // Setup
            var errorResponse1 = new ErrorResponse(_code, _message);
            var errorResponse2 = new ErrorResponse(_code, _message);
            var errorResponse3 = new ErrorResponse();
            var errorResponse4 = new ErrorResponse(0, string.Empty);

            // Function Call
            var result1 = errorResponse1.Equals(errorResponse2);
            var result2 = errorResponse3.Equals(errorResponse4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.True);
                Assert.That(result2, Is.True);
            });
        }

        [Test]
        public void ErrorResponse_Equals_NotEqualObjects_ReturnsTrue()
        {
            // Setup
            var errorResponse1 = new ErrorResponse(_code, _message);
            var errorResponse2 = new ErrorResponse(_code, _message);
            var errorResponse3 = new ErrorResponse();
            var errorResponse4 = new ErrorResponse(0, string.Empty);

            // Function Call
            var result1 = errorResponse1.Equals(errorResponse3);
            var result2 = errorResponse1.Equals(errorResponse4);
            var result3 = errorResponse2.Equals(errorResponse3);
            var result4 = errorResponse2.Equals(errorResponse4);

            // Assertions
            Assert.Multiple(() => {
                Assert.That(result1, Is.False);
                Assert.That(result2, Is.False);
                Assert.That(result3, Is.False);
                Assert.That(result4, Is.False);
            });
        }

        [Test]
        public void ErrorResponse_GetHashCode_EqualObjects_ReturnsSameHashCodes()
        {
            // Setup
            var expected = new ErrorResponse(_code, _message).GetHashCode();
            var errorResponse = new ErrorResponse(_code, _message);

            // Function Call
            var result = errorResponse.GetHashCode();

            // Assertions
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ErrorResponse_GetHashCode_NotEqualObjects_ReturnsDifferentHashCodes()
        {
            // Setup
            var expected = new ErrorResponse(_code, _message).GetHashCode();

            var errorResponse1 = new ErrorResponse(0, _message);
            var errorResponse2 = new ErrorResponse(_code, "Another message.");
            var errorResponse3 = new ErrorResponse(0, "Another message.");

            // Function Call
            var result1 = errorResponse1.GetHashCode();
            var result2 = errorResponse2.GetHashCode();
            var result3 = errorResponse3.GetHashCode();

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
