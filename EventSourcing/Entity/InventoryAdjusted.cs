using EventSourcing.Event;

namespace EventSourcing.Entity
{
    public record InventoryAdjusted(string Sku, string Reason, int Quantity, DateTime DateTime) : IEvent;
}
