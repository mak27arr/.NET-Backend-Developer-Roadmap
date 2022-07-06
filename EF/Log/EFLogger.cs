using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EF.Log
{
    internal class EFLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) => Debug.WriteLine(formatter(state, exception));
    }
}