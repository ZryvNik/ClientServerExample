using ServerExample.App.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(
    typeof(OutputMessageDto).Assembly,
    typeof(OutputMessageEvent).Assembly);
builder.Services.AddMassTransit(x =>
{
    var config = builder.Configuration.GetSection("RabbitMqConnection").Get<RabbitMqConnectionConfig>();
    if (config == null)
        x.UsingInMemory((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    else
        x.UsingRabbitMq((context, bus) =>
        {
            bus.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            bus.ConfigureEndpoints(context);
        });
});
builder.Services.AddLogging();
builder.Services.AddSingleton<IBusPublisher, BusPublisher>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/Generate", async (GenerateOutputMessagesRequest request, IMediator mediatr) =>
    await mediatr.Send(request));

app.Run();