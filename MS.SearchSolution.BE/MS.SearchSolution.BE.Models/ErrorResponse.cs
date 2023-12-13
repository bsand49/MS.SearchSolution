using System.Runtime.Serialization;

namespace MS.SearchSolution.BE.Models
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        #region Constructors
        public ErrorResponse()
        {
            Message = string.Empty;
        }
        public ErrorResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            if (obj is not ErrorResponse)
                return false;

            return Equals((ErrorResponse)obj);
        }
        private bool Equals(ErrorResponse errorResponse)
        {
            return
                Code.Equals(errorResponse.Code) &&
                Message.Equals(errorResponse.Message);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message);
        }
        #endregion
    }
}
