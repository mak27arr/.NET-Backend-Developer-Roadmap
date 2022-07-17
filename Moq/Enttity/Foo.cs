using System;
using System.Threading.Tasks;

namespace MoqL.Enttity
{
    internal class Foo : IFoo
    {
        public Bar Bar { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public virtual EventHandler FooEvent { get; set; }

        public virtual event MyEventHandler MyEvent;

        public bool Add(int value) => true;

        public bool DoSomething(string value) => false;

        public bool DoSomething(int number, string value) => false;

        public Task<bool> DoSomethingAsync() => Task.FromResult(true);

        public string DoSomethingStringy(string value) => String.Empty;

        public int GetCount() => 0;

        public bool Submit(ref Bar bar) => false;

        public bool TryParse(string value, out string outputValue)
        {
            outputValue = String.Empty;
            return true;
        }
    }
}
