using SchedulerService.Exceptions;
using SchedulerService.Models;
using SchedulerService.Models.Enums;
using SchedulerService.Services.ConsumeConfiguration.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Services.ConsumeConfiguration.Service
{
    internal class JobScheduler : IJobScheduler
    {
        private List<Job> jobs = new List<Job>();
        private readonly ILogger<JobScheduler> _logger;
        public JobScheduler(ILogger<JobScheduler> logger)
        {

            _logger = logger;

        }
        public void CreateJob(ExecutionConfiguration configuration)
        {
            DateTime? nextExecutionTime = null; 
            if (configuration.executionTime == null)
                nextExecutionTime = ScheduleResolver(
                    DateTime.Now, 
                    configuration.executionUnit, 
                    configuration.executionFrequency);
            else
                nextExecutionTime = ScheduleResolver(
                    configuration.executionTime.Value,
                    configuration.executionUnit,
                    configuration.executionFrequency);

            jobs.Add(new Job(nextExecutionTime.Value, configuration.path));
        }
        public IEnumerable<Job> GetScheduledJobs()
        {
            return jobs;
        }

        public DateTime ScheduleResolver(DateTime baseTime, string unresolvedExecutionUnit, long executionFrequency)
        {
            DateTime? executionTime = null;

            ExecutionUnit executionUnit = ExecutionUnitResolver(unresolvedExecutionUnit);

            switch (executionUnit)
            {
                case Models.Enums.ExecutionUnit.Year:
                    executionTime = baseTime.AddDays(365 / executionFrequency);
                    break;
                case Models.Enums.ExecutionUnit.Month:
                    executionTime = baseTime.AddDays(30 / executionFrequency);
                    break;
                case Models.Enums.ExecutionUnit.Week:
                    executionTime = baseTime.AddDays(7 / executionFrequency);
                    break;
                case Models.Enums.ExecutionUnit.Day:
                    executionTime = baseTime.AddHours(24 / executionFrequency);
                    break;
                case Models.Enums.ExecutionUnit.Hour:
                    executionTime = baseTime.AddHours(60 / executionFrequency);
                    break;
                case Models.Enums.ExecutionUnit.Minute:
                    executionTime = baseTime.AddMinutes(60 / executionFrequency);
                    break;
                default:
                    break;
            }

            if (executionTime == null)
            {
                string message = "Unable to resolve execution time.";
                _logger.LogError(message);
                throw new UnresolvedExecutionTimeException(message);
            }

            return executionTime.Value;
        }

        public void RemoveJob(Guid jobId)
        {
            jobs.Remove(jobs.First(j => j.JobId == jobId));
        }

        public ExecutionUnit ExecutionUnitResolver(string executionUnitValue)
        {
            ExecutionUnit? executionUnit = executionUnitValue.StringToExecutionUnit();
            if (executionUnit == null)
            {
                string formatMessage = string.Format("Unable to resolve execution unit. Provided value: {0}." ,executionUnitValue);
                _logger.LogError(formatMessage);
                throw new UnresolvedExecutionUnitException(formatMessage);
            }

            return executionUnitValue.StringToExecutionUnit().Value;
        }
            
    }
}
