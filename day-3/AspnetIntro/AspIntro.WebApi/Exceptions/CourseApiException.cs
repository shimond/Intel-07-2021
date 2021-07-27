using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.Exceptions
{

    [Serializable]
    public class CourseApiException: Exception
    {
        public ApiExceptionCodes ErrorCode { get; set; }
        public CourseApiException(ApiExceptionCodes ErrorCode): base() { }
        public CourseApiException(string message) : base(message) { }
        public CourseApiException(string message, Exception inner) : base(message, inner) { }
        protected CourseApiException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    public enum ApiExceptionCodes
    {
        NotFound, Conflict
    }
}
