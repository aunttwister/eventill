using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Exceptions
{
    public class UnresolvedExecutionUnitException : Exception
    {
        public UnresolvedExecutionUnitException(string message) : base(message) { }
    }
}
