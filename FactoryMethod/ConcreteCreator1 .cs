namespace FactoryMethod
{
    internal class ConcreteCreator1 : Creator
    {
        public override IProduct FactoryMethod() => new ConcreteProduct1();
    }
}
