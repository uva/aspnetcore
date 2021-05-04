using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Logging.W3C
{
    internal class W3CLoggerProcessor : IDisposable
    {
        private const int _maxQueuedMessages = 1024;

        private readonly string _path;
        private readonly string _fileName;
        private readonly int? _maxFileSize;

        private readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(_maxQueuedMessages);
        private Task _outputTask;

        public W3CLoggerProcessor(IOptionsMonitor<W3CLoggerOptions> options)
        {
            var loggerOptions = options.CurrentValue;
            _path = loggerOptions.LogDirectory;
            _fileName = loggerOptions.FileName;
            _maxFileSize = loggerOptions.FileSizeLimit;

            Directory.CreateDirectory(_path);

            WriteDirectives();

            // Start W3C message queue processor
            _outputTask = Task.Run(ProcessLogQueue);
        }

        public void EnqueueMessage(string message)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                try
                {
                    _messageQueue.Add(message);
                    return;
                }
                catch (InvalidOperationException) { }
            }
        }

        internal async Task WriteMessageAsync(string entry, StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync(entry);
            await streamWriter.FlushAsync();
        }

        private async Task ProcessLogQueue()
        {
            try
            {
                var fullName = Path.Combine(_path, $"{_fileName}{Guid.NewGuid().ToString()}.log");
                using (var streamWriter = File.AppendText(fullName))
                {
                    foreach (string message in _messageQueue.GetConsumingEnumerable())
                    {
                        await WriteMessageAsync(message, streamWriter);
                    }
                }
            }
            catch
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch { }
            }
        }

        public void Dispose()
        {
        }

        private void WriteDirectives()
        {
            EnqueueMessage("#Version: 1.0");

            var startTimeBuilder = new StringBuilder();
            startTimeBuilder.Append("#Start-Date: ");
            startTimeBuilder.Append(DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            EnqueueMessage(startTimeBuilder.ToString());
        }
    }
}
