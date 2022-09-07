namespace Flyweight
{
    internal class FlyweightFactory
    {
        private Dictionary<string, Flyweight> _flyweights = new Dictionary<string, Flyweight>();

        public FlyweightFactory(params Car[] cars)
        {
            foreach (var elem in cars)
                _flyweights.Add(GetKey(elem), new Flyweight(elem));
        }

        public string GetKey(Car car)
        {
            List<string> elements = new List<string>();

            elements.Add(car.Model);
            elements.Add(car.Color);
            elements.Add(car.Company);

            if (car.Owner != null && car.Number != null)
            {
                elements.Add(car.Number);
                elements.Add(car.Owner);
            }

            elements.Sort();

            return string.Join("_", elements);
        }

        public Flyweight GetFlyweight(Car sharedState)
        {
            var key = GetKey(sharedState);

            if (!_flyweights.ContainsKey(key))
            {
                Console.WriteLine("FlyweightFactory: Can't find a flyweight, creating new one.");
                _flyweights.Add(key, new Flyweight(sharedState));
            }
            else
            {
                Console.WriteLine("FlyweightFactory: Reusing existing flyweight.");
            }

            return _flyweights[key];
        }

        public void ListFlyweights()
        {
            var count = _flyweights.Count;
            Console.WriteLine($"\nFlyweightFactory: I have {count} flyweights:");

            foreach (var flyweight in _flyweights)
                Console.WriteLine(flyweight.Key);
        }
    }
}
