using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Microsoft.Extensions.Logging.W3C
{
    internal class W3CLoggerProcessor : IDisposable
    {
        private const int _maxQueuedMessages = 1024;

        private readonly BlockingCollection<string> _messageQueue = new BlockingCollection<string>(_maxQueuedMessages);
        private readonly Thread _outputThread;

        public W3CLoggerProcessor()
        {
            // Start W3C message queue processor
            _outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "W3C logger queue processing thread"
            };
            _outputThread.Start();
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

        internal void WriteMessage(string entry)
        {
            console.Write(entry.Message);
        }

        private void ProcessLogQueue()
        {
            try
            {
                foreach (string message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
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
            throw new NotImplementedException();
        }
    }
}
