namespace FactoryMethod
{
    internal class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod() => new ConcreteProduct2();
    }
}
