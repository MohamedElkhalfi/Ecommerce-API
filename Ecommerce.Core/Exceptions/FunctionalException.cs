using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Exceptions
{
    public class FunctionalException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public HttpStatusCode HttpStatusCode { get; }

        public FunctionalException()
        {
        }

        public FunctionalException(string message)
            : base(message)
        {
        }

        public FunctionalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        

        public FunctionalException(string message, string code) : this(message, code, HttpStatusCode.InternalServerError)
        {
            ErrorMessage = message;
            ErrorCode = code;
        }

        public FunctionalException(string message, string code, HttpStatusCode httpStatusCode)
            : base(message)
        {
            ErrorMessage = message;
            ErrorCode = code;
            HttpStatusCode = httpStatusCode;
        }
    }
}
