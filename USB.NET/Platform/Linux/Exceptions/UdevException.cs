using System;
using System.Runtime.Serialization;

namespace USB.NET.Platform.Linux.Exceptions
{
    [Serializable]
    public class UdevException : Exception
    {
        public UdevException()
        {
        }
        public UdevException(string message) : base(message)
        {
        }

        public UdevException(string message, Exception inner) : base(message, inner)
        {            
        }

        protected UdevException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}