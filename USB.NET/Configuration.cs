using System.Collections;
using System.Collections.Generic;

namespace USB.NET
{
    public abstract class Configuration
    {
        public abstract uint InterfaceCount { get; }
        public abstract Interface GetInterface(int index);
    }
}