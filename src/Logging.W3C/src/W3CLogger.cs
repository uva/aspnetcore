using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;
using System.Diagnostics;
using System.Collections.Generic;

namespace Microsoft.Extensions.Logging.W3C
{
    internal sealed class W3CLogger : ILogger
    {

        private readonly string _name;
        private readonly W3CLoggerProcessor _messageQueue;
        private readonly IOptionsMonitor<W3CLoggerOptions> _options;

        internal W3CLogger(string name, IOptionsMonitor<W3CLoggerOptions> options)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _name = name;
            _options = options;
            _messageQueue = new W3CLoggerProcessor(_options);
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
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (state is IReadOnlyCollection<KeyValuePair<string, object>> stateProperties)
            {
                foreach (KeyValuePair<string, object> item in stateProperties)
                {
                    _messageQueue.EnqueueMessage(item.ToString());
                }
            }
        }
    }
}
