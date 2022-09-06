namespace DecoratorP
{
    internal class Client
    {
        public void ClientCode(IComponent component) => Console.WriteLine("RESULT: " + component.Operation());
    }
}
