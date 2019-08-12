using System.Collections.Generic;
using System.Net;

namespace Elmah.Io.Client
{
    public class ElmahIoOptions
    {
        public List<string> FormKeysToObfuscate { get; set; }

        public IWebProxy WebProxy { get; set; }

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
