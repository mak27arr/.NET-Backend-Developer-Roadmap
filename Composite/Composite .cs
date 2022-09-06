namespace Composite
{
    internal class CompositeBase : Component
    {
        protected List<Component> _children = new List<Component>();

        public override void Add(Component component) => _children.Add(component);

        public override void Remove(Component component) => _children.Remove(component);

        public override string Operation() =>  $"Branch( {string.Join("; ", _children.Select(x => x.Operation()).Where(rez => !string.IsNullOrEmpty(rez)))} )";

        public override bool IsComposite() => true;
    }
}
