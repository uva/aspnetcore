using System;
using System.Diagnostics;

namespace Microsoft.Extensions.Logging.W3C
{
    internal sealed class W3CLogger : ILogger
    {

        private readonly string _name;

        internal W3CLogger(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _name = name;
            _hasWrittenDirectives = false;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debugger.Launch();
            Debugger.Break();
            if (!IsEnabled(logLevel))
            {
                return;
            }

            throw new NotImplementedException();
        }
    }
}
