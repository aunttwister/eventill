using SchedulerService.Models;
using SchedulerService.Models.Interfaces;
using SchedulerService.Services.ConsumeConfiguration.Interface;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SchedulerService
{
    public class Worker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationConsumer _consumeConfiguration;
        private readonly IJobScheduler _jobScheduler;

        public Worker(ILogger<Worker> logger, 
            IHostApplicationLifetime lifetime, 
            IConfigurationConsumer consumeConfiguration,
            IJobScheduler jobScheduler)
        {
            _logger = logger;
            _lifetime = lifetime;
            _consumeConfiguration = consumeConfiguration;
            _jobScheduler = jobScheduler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduler service has started.");
            await LoadConfiguration();
            IEnumerable<Job> scheduledJobs = _jobScheduler.GetScheduledJobs();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                if (scheduledJobs.Any(sj => sj.ScheduledTime == DateTime.Now)) 
                {
                    IEnumerable<Job> toExecute = scheduledJobs.Where(sj => sj.ScheduledTime == DateTime.Now);
                    foreach (Job job in toExecute)
                    {
                        try
                        {
                            Process.Start(job.Path);
                        }
                        catch (Exception ex)
                        {

                            _logger.LogError(ex.Message);
                        }

                        _jobScheduler.RemoveJob(job.JobId);
                        scheduledJobs = _jobScheduler.GetScheduledJobs();
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task LoadConfiguration()
        {
            List<ExecutionConfiguration> configs = await _consumeConfiguration.ReadConfiguration();
            foreach (var config in configs)
            {
                _jobScheduler.CreateJob(config);
            }
        }
    }
}