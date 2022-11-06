using MassTransit;
using Shared;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();
