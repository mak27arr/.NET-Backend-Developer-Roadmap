using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using myCloudDAL.DAL.Entities.Identity;
using Newtonsoft.Json;

namespace DAL.Logger
{
    public class UserManagerLogger : ILogger<UserManager<AppUser>>
    {
        private string _loggingContext;

        public IDisposable BeginScope<TState>(TState state)
        {
            var s = state as IDisposable;

            _loggingContext = JsonConvert.SerializeObject(s);

            return s;
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel > LogLevel.Warning;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Console.WriteLine($"{logLevel} : {eventId} : {state} : {exception.Message} : {_loggingContext}");
        }
    }
}
