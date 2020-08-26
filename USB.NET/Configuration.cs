using System.Collections;
using System.Collections.Generic;
using USB.NET.Descriptors;

namespace USB.NET
{
    public abstract class Configuration
    {
        protected void SetValues(ConfigurationDescriptor descriptor)
        {
            this.ConfigurationValue = descriptor.bConfigurationValue;
            this.InterfaceCount = descriptor.bNumInterfaces;
        }

        public virtual uint ConfigurationValue { protected set; get; }
        public virtual uint InterfaceCount { protected set; get; }
        
        public abstract bool SetInterface(ushort index);
        public abstract Interface GetInterface();
        
        public abstract ConfigurationDescriptor GetConfigurationDescriptor();
    }
}