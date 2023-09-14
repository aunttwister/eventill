using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Models.Enums
{
    public enum ExecutionUnit
    {
        Year, Month, Week, Day, Hour, Minute
    }

    public static class ExecutionUnitExtensions
    {
        public static ExecutionUnit? StringToExecutionUnit(this string value)
        {
            var lowerCase = value.ToLower();
            switch (lowerCase)
            {
                case "minute":
                    return ExecutionUnit.Minute;
                case "hour":
                    return ExecutionUnit.Hour;
                case "day":
                    return ExecutionUnit.Day;
                case "week":
                    return ExecutionUnit.Week;
                case "month":
                    return ExecutionUnit.Month;
                case "year":
                    return ExecutionUnit.Year;
                default:
                    return null;
            }
        }
    }
}
