var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(
    typeof(InputMessageDto).Assembly,
    typeof(InputMessage).Assembly,
    typeof(InputMessageProcessingHandler).Assembly);

var connectionString = builder.Configuration.GetConnectionString("InputMessages");
var rabbitMqConfig = builder.Configuration.GetSection("RabbitMqConnection").Get<RabbitMqConnectionConfig>();
var backConfig = builder.Configuration.GetSection("BackgroundWorking").Get<BackgroundWorkingConfiguration>();

builder.Services.AddDbContext<InputMessagesContext>(opt =>
{
    if (string.IsNullOrEmpty(connectionString))
        opt.UseInMemoryDatabase("InputMessages");
    else
        opt.UseSqlServer(connectionString);
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OutputMessageEventConsumer>();
    if (rabbitMqConfig == null)
        x.UsingInMemory((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    else
        x.UsingRabbitMq((context, bus) =>
        {
            bus.Host(rabbitMqConfig.HostName, "/", h =>
            {
                h.Username(rabbitMqConfig.Username);
                h.Password(rabbitMqConfig.Password);
            });

            bus.ConfigureEndpoints(context);
        });
});
builder.Services.AddHostedService<InputMessageProcessingService>();
builder.Services.AddSingleton<IBackgroundWorkingConfiguration>(backConfig);
builder.Services.AddTransient<Mediator>();
builder.Services.AddSingleton<ISender, ScopedSender<Mediator>>();
var app = builder.Build();
app.Run();