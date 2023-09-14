using SchedulerService;
using SchedulerService.Services.ConsumeConfiguration.Interface;
using SchedulerService.Services.ConsumeConfiguration.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        RegisterServices(services);
        services.AddHostedService<Worker>();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddFile();
    })
    .Build();

host.Run();

static void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<IJobScheduler, JobScheduler>();
    services.AddSingleton<IConfigurationConsumer, ConfigurationConsumer>();
}
