using EventSourcing.Event;

namespace EventSourcing.Repo
{
    internal class WarehouseProductRepository
    {
        private readonly Dictionary<string, IList<IEvent>> _inMemory = new ();

        public WarehouseProduct Get(string sku)
        {
            var wareProduct = new WarehouseProduct(sku);

            if (_inMemory.ContainsKey(sku))
                foreach (var evnt in _inMemory[sku])
                    wareProduct.AddEvent(evnt);

            return wareProduct;
        }

        public void Save(WarehouseProduct product) => _inMemory[product.Sku] = product.GetEvents();

    }
}
