using EventSourcing.Entity;
using EventSourcing.Event;

namespace EventSourcing.Repo
{
    public class WarehouseProduct
    {
        public string Sku { get; }
        private readonly IList<IEvent> _event = new List<IEvent>();
        private readonly CurrentState _currentState = new();

        public WarehouseProduct(string sku)
        {
            Sku = sku;
        }

        public void ShipProduct(int quantity)
        {
            if (quantity > _currentState.QuantityOnHand)
                throw new InvalidOperationException("We don`t have enough product");

            AddEvent(new ProductShipped(Sku, quantity, DateTime.Now));
        }

        public void ReceiveProduct(int quantity) => AddEvent(new ProductReceived(Sku, quantity, DateTime.Now));

        public void AdjustInventory(int quantity, string reason)
        {
            if (quantity + _currentState.QuantityOnHand < 0)
                throw new InvalidOperationException("Cannot adjust");

            AddEvent(new InventoryAdjusted(Sku, reason, quantity, DateTime.Now));
        }

        private void Apply(ProductShipped evnt) => _currentState.QuantityOnHand -= evnt.Quantity;

        private void Apply(ProductReceived evnt) => _currentState.QuantityOnHand += evnt.Quantity;

        private void Apply(InventoryAdjusted evnt) => _currentState.QuantityOnHand += evnt.Quantity;

        public void AddEvent(IEvent evnt)
        {
            switch (evnt)
            {
                case ProductShipped productShipped:
                    Apply(productShipped);
                    break;
                case ProductReceived productReceived:
                    Apply(productReceived);
                    break;
                case InventoryAdjusted inventoryAdjusted:
                    Apply(inventoryAdjusted);
                    break;
            }

            _event.Add(evnt);
        }

        public IList<IEvent> GetEvents() => _event;
    }
}