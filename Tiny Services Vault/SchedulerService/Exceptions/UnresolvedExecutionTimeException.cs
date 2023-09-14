using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Exceptions
{
    public class UnresolvedExecutionTimeException : Exception
    {
        public UnresolvedExecutionTimeException(string message) : base(message) { }
    }
}
