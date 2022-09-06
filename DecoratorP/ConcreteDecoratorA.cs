namespace DecoratorP
{
    internal class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(IComponent comp) : base(comp)
        {}

        public override string Operation() => $"ConcreteDecoratorA({base.Operation()})";
    }
}
