using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Exceptions
{
    [Serializable]
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message) { }

        protected AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
