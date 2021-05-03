using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Logging.W3C
{
    public class W3CLoggerProvider : ILoggerProvider
    {

        private readonly IOptionsMonitor<W3CLoggerOptions> _options;
        private readonly ConcurrentDictionary<string, W3CLogger> _loggers;

        public W3CLoggerProvider(IOptionsMonitor<W3CLoggerOptions> options)
        {
            _options = options;
            _loggers = new ConcurrentDictionary<string, W3CLogger>();
        }

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
