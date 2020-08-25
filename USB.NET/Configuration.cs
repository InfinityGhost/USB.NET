using System.Collections;
using System.Collections.Generic;
using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Configuration
    {
        protected void SetValues(ConfigurationDescriptor descriptor)
        {
            this.InterfaceCount = descriptor.bNumInterfaces;
        }

        public abstract uint InterfaceCount { protected set; get; }
        public abstract Interface GetInterface(int index);
        
        public abstract ConfigurationDescriptor GetConfigurationDescriptor();
    }
}