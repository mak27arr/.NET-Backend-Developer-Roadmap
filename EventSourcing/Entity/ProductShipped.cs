using EventSourcing.Event;

namespace EventSourcing.Entity
{
    public record ProductShipped(string Sku, int Quantity, DateTime DateTime) : IEvent;
}
