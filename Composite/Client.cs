﻿namespace Composite
{
    internal class Client
    {
        public void ClientCode(Component leaf) => Console.WriteLine($"RESULT: {leaf.Operation()}");

        // Thanks to the fact that the child-management operations are declared
        // in the base Component class, the client code can work with any
        // component, simple or complex, without depending on their concrete
        // classes.
        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
                component1.Add(component2);

            Console.WriteLine($"RESULT: {component1.Operation()}");
        }
    }
}
