using System.Collections.Generic;
using System.Net;

namespace Elmah.Io.Client
{
    /// <summary>
    /// Options to use when using the elmah.io client.
    /// </summary>
    public class ElmahIoOptions
    {
        /// <summary>
        /// Zero or more form key names to obfuscate before logging messages to elmah.io.
        /// </summary>
        public List<string> FormKeysToObfuscate { get; set; }

        /// <summary>
        /// Make it possible to communicate with the elmah.io API through a proxy.
        /// </summary>
        public IWebProxy WebProxy { get; set; }

        /// <summary>
        /// Create a new instance with default options.
        /// </summary>
        public ElmahIoOptions()
        {
            FormKeysToObfuscate = new List<string>
            {
                "password",
                "pwd"
            };
        }
    }
}
