using System;
using System.Runtime.Serialization;

namespace Reservations.Application.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
