using Microsoft.Extensions.Logging;

namespace EF.Log
{
    internal class EFLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) => throw new NotSupportedException();

        public ILogger CreateLogger(string categoryName) => new EFLogger();

        public void Dispose(){ }
    }
}
