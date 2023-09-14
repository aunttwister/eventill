using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Exceptions
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException(string message) : base(message) { }
    }
}
