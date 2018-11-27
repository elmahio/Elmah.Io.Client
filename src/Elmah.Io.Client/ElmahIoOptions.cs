using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.Io.Client
{
    public class ElmahIoOptions
    {
        public List<string> FormKeysToObfuscate { get; set; }

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
