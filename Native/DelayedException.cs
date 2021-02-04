using System;

namespace Native
{
    public class DelayedException<T> where T : Exception, new()
    {
        public DelayedException(string message)
        {
            Exception = (T)Activator.CreateInstance(typeof(T), message);
        }

        private readonly T Exception;
        public static implicit operator Exception(DelayedException<T> e) => e.Exception;
    }
}