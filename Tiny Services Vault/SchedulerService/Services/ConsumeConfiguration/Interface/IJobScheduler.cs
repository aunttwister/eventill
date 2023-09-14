using SchedulerService.Models;
using SchedulerService.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Services.ConsumeConfiguration.Interface
{
    public interface IJobScheduler
    {
        public void CreateJob(ExecutionConfiguration configuration);
        public IEnumerable<Job> GetScheduledJobs();
        public DateTime ScheduleResolver(DateTime baseTime, string executionUnit, long executionFrequency);
        public void RemoveJob(Guid jobId);
    }
}
