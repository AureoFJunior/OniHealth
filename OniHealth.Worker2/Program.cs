using OniHealth.Worker2;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WorkerLateConsults>();
    })
    .Build();

await host.RunAsync();
