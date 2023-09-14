using SchedulerService.Models.Enums;
using SchedulerService.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Models
{
    public class ExecutionConfiguration
    {
        public string path { get; set; }
        public string arguments { get; set; }
        public DateTime? executionTime { get; set; }
        public string executionUnit { get; set; }
        public int executionFrequency { get; set; }
    }
}
