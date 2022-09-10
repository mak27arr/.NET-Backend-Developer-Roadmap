namespace AbstractFactory
{
    internal class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA() => new ConcreteProductA1();

        public IAbstractProductB CreateProductB() => new ConcreteProductB1();
    }
}
