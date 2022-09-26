using AbstractFactory.ProductB;

namespace AbstractFactory
{
    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA() => new ConcreteProductA2();

        public IAbstractProductB CreateProductB() => new ConcreteProductB2();
    }
}
