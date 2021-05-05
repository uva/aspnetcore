using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging.W3C
{
    /// <summary>
    /// Flags used to control which parts of the
    /// request and response are logged in W3C format.
    /// </summary>
    [Flags]
    public enum W3CLoggingFields : long
    {
        /// <summary>
        /// No logging.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Flag for logging the date
        /// that the activity occurred.
        /// </summary>
        Date = 0x1,

        /// <summary>
        /// Flag for logging the time
        /// that the activity occurred.
        /// </summary>
        Time = 0x2,

        /// <summary>
        /// Flag for logging the IP address
        /// of the client that accessed the server.
        /// </summary>
        ClientIpAddress = 0x4,

        /// <summary>
        /// Flag for logging the name of the
        /// authenticated user that accessed the server.
        /// </summary>
        UserName = 0x8,

        /// <summary>
        /// Flag for logging the name of the
        /// authenticated user that accessed the server.
        /// </summary>
        ServiceName = 0x10,

        /// <summary>
        /// Flag for logging the name of the
        /// server on which the log entry was generated.
        /// </summary>
        ServerName = 0x20,

        ServerIpAddress = 0x40,

        ServerPort = 0x80,

        Method = 0x100,

        UriStem = 0x200,

        UriQuery = 0x400,

        ProtocolStatus = 0x800,

        BytesSent = 0x1000,

        BytesReceived = 0x2000,

        TimeTaken = 0x4000,

        ProtocolVersion = 0x8000,

        Host = 0x10000,

        UserAgent = 0x20000,

        Cookie = 0x40000,

        Referrer = 0x80000
    }
}
