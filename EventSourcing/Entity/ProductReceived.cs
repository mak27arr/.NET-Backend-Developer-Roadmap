using EventSourcing.Event;

namespace EventSourcing.Entity
{
    public record ProductReceived(string Sku, int Quantity, DateTime DateTime) : IEvent;
}
