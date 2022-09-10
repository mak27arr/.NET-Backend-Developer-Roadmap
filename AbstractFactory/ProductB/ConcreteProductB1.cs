namespace AbstractFactory.ProductB
{
    internal class ConcreteProductB1 : IAbstractProductB
    {
        public string UsefulFunctionB() => "The result of the product B1.";

        public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }
}
