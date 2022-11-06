using MassTransit;

namespace Shared;

public class TicketConsumer : IConsumer<Ticket>
{
    public Task Consume(ConsumeContext<Ticket> context)
    {
        // do some magic...
        Task.Delay(50000);
        return Task.CompletedTask;
    }
}
