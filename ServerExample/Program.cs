var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(
    typeof(OutputMessageDto).Assembly,
    typeof(OutputMessage).Assembly,
    typeof(OutputMessageEvent).Assembly);
builder.Services.AddDbContext<OutputMessagesContext>(opt => opt.UseInMemoryDatabase("OutputMessages"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OutputMessageEventConsumer>();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSingleton<IBusPublisher, BusPublisher>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/test/Add", async (AddOutputMessageRangeRequest request, IMediator mediatr) =>
    await mediatr.Send(request));
app.MapPost("/test/Generate", async (GenerateOutputMessagesRequest request, IMediator mediatr) =>
    await mediatr.Send(request));
app.MapGet("/test/GetUnSentOutputMessages", async (int skip, int take, IMediator mediatr) =>
    await mediatr.Send(new GetUnSentOutputMessagesRequest(skip, take)));

app.Run();

