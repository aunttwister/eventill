using SchedulerService.Exceptions;
using SchedulerService.Models;
using SchedulerService.Models.Interfaces;
using SchedulerService.Services.ConsumeConfiguration.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Services.ConsumeConfiguration.Service
{
    internal class ConfigurationConsumer : IConfigurationConsumer
    {
        private readonly ILogger<ConfigurationConsumer> _logger;

        public ConfigurationConsumer(ILogger<ConfigurationConsumer> logger)
        {
            _logger = logger;
        }

        async Task<List<ExecutionConfiguration>> IConfigurationConsumer.ReadConfiguration()
        {
            string[] configsPath = Directory.GetFiles("./config/", "*.json");

            List<ExecutionConfiguration> configs = new List<ExecutionConfiguration>();
            foreach (string path in configsPath)
            {
                configs.Add(await LoadJson(path));
            }

            return configs;
        }

        public async Task<ExecutionConfiguration> LoadJson(string path)
        {
            string jsonPath = null;
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    jsonPath = await reader.ReadToEndAsync();
                }
                catch (Exception ex)
                {
                    string formatException = string.Format("Invalid path exception.\nPath: {0}\nException: {1}", jsonPath, ex.Message);
                    _logger.LogCritical(ex.Message);
                    throw new InvalidPathException(formatException);
                }
            }

            ExecutionConfiguration json = null;
            try
            {
                json = JsonConvert.DeserializeObject<ExecutionConfiguration>(jsonPath);
            }
            catch (Exception ex)
            {
                string formatException = string.Format("Invalid format of json file.\nPath: {0}\nException: {1}", json, ex.Message);
                _logger.LogCritical(formatException);
                throw new FormatException(formatException);
            }

            return json;
        }
    }
}
