Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<InputMessageProcessingService>();
        services.AddMediatR(
            typeof(InputMessageDto).Assembly,
            typeof(InputMessage).Assembly,
            typeof(InputMessageProcessingHandler).Assembly);
        services.AddDbContext<InputMessagesContext>(opt => opt.UseInMemoryDatabase("InputMessages"));
    })
    .Build()
    .Run();
