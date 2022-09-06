namespace DecoratorP
{
    internal abstract class Decorator : IComponent
    {
        protected IComponent _component;

        public Decorator(IComponent component)
        {
            this._component = component;
        }

        public void SetComponent(IComponent component) => _component = component;

        public virtual string Operation() => _component != null ? _component.Operation() : string.Empty;
    }
}
