using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.Io.Client
{
    public partial interface IElmahioAPI
    {
        ElmahIoOptions Options { get; set; }
    }
}
