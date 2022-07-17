using Moq;
using MoqL.Enttity;
using System;
using System.Runtime.CompilerServices;
using Xunit;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace MoqL
{
    public class UnitTests
    {
        [Fact]
        public void TestDo()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            Assert.True(mock.Object.DoSomething("ping"));
            Assert.False(mock.Object.DoSomething("true"));
        }

        [Fact]
        public void TestEx()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(() => mock.Object.DoSomething("reset"));
        }

        [Fact]
        public void TestProp()
        {
            var name = "bar";
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.Name).Returns(name);
            mock.SetupSet(foo => foo.Value = 10);

            Assert.Equal(name, mock.Object.Name);
            Assert.Equal(10, mock.Object.Value);
        }

        [Fact]
        public void TestEvent()
        {
            var name = "bar";
            var mock = new Mock<Foo>();
            mock.Raise(foo => foo.MyEvent += null, 25, true);
        }

        [Fact]
        public void TestVerify()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);
            mock.Object.DoSomething("ping");
            mock.Verify(foo => foo.DoSomething("ping"), Times.Once);
            mock.Object.DoSomething("ping");
            mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeast(2));
        }
    }
}