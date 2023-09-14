using SchedulerService.Models;
using SchedulerService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Services.ConsumeConfiguration.Interface
{
    public interface IConfigurationConsumer
    {
        internal Task<List<ExecutionConfiguration>> ReadConfiguration();
    }
}
