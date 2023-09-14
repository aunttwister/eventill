using SchedulerService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Models
{
    public class Job
    {
        public Job(DateTime nextExecutionTime, string path)
        {
            JobId = Guid.NewGuid();
            ScheduledTime = nextExecutionTime;
            Path = path;
        }
        public Guid JobId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Path { get; set; }
    }
}
