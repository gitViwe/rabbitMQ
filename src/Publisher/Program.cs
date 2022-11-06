using MassTransit;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", host =>
        {
            host.Username("guest");
            host.Password("guest");
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

app.MapPost("/create-ticket", async Task<IResult> (Ticket ticket, IBus bus) =>
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
