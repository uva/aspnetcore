using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.Logging.W3C
{
    public class W3CLoggerProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, W3CLogger> _loggers;

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.TryGetValue(categoryName, out W3CLogger logger) ?
                logger :
                _loggers.GetOrAdd(categoryName, new W3CLogger(categoryName));
        }

        public void Dispose()
        {
        }
    }
}
