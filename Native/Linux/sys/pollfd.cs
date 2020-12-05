namespace Native.Linux.sys
{
    public struct pollfd
    {
        /// <summary>
        /// File descriptor to poll.
        /// </summary>
        public int fd;
        /// <summary>
        /// Types of events poller cares about.
        /// </summary>
        public pollevent events;
        /// <summary>
        /// Types of events that actually occured.
        /// </summary>
        public pollevent revents;
    }
}