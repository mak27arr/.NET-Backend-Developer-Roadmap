namespace DecoratorP
{
    internal class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(IComponent comp) : base(comp)
        { }

        public override string Operation() => $"Ololo ConcreteDecoratorB({base.Operation()})";
    }
}
