using Ecommerce.Core.Exceptions.Constants;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Ecommerce.Core.Exceptions
{
   
        [ExcludeFromCodeCoverage]
        [Serializable]
        public class TechnicalException : Exception
        {
            public string Code { get; }
            public HttpStatusCode HttpStatusCode { get; }

            public TechnicalException()
            {
            }

            public TechnicalException(Exception innerException)
                : this(MessagesConstantes.TECHNICAL_INTERNAL_ERROR, innerException)
            {
            }

            public TechnicalException(string message)
                : base(message)
            {

            }

            public TechnicalException(string message, Exception innerException)
                : base(message, innerException)
            {
                HttpStatusCode = HttpStatusCode.InternalServerError;
            }

            public TechnicalException(string message, string code, HttpStatusCode httpsStatusCode)
                : base(message)
            {
                Code = code;
                HttpStatusCode = httpsStatusCode;
            }

            protected TechnicalException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

        }
    }
 
