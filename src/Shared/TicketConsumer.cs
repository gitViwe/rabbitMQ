using MassTransit;
using System.Text.Json;

namespace Shared;

public class TicketConsumer : IConsumer<Ticket>
{
    public Task Consume(ConsumeContext<Ticket> context)
    {
        // do some magic...
        Console.WriteLine(JsonSerializer.Serialize(context.Message));
        return Task.CompletedTask;
    }
}
