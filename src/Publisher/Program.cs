using MassTransit;
using Shared;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumers(typeof(TicketConsumer).Assembly);
    configure.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        config.ReceiveEndpoint("ticketQueue", endpoint =>
        {
            endpoint.PrefetchCount = 16;
            endpoint.UseMessageRetry(retry =>
            {
                retry.Interval(3, 500);
            });
            endpoint.ConfigureConsumer<TicketConsumer>(context);
        });
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/create-ticket", async Task<IResult>(Ticket ticket, IBus bus) =>
{
    if (ticket != null)
    {
        ticket.BookedOn = DateTime.Now;
        Uri uri = new("rabbitmq://localhost/ticketQueue");
        for (int i = 0; i < 15; i++)
        {
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(ticket);
        }
        return Results.Ok("message published");
    }
    return Results.BadRequest();
})
.WithName("CreateTicket");

app.Run();
