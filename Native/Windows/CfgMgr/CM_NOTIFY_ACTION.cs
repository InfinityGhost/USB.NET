namespace Native.Windows
{
    public static partial class CfgMgr
    {
        public enum CM_NOTIFY_ACTION
        {
            DEVICEINTERFACEARRIVAL,
            DEVICEINTERFACEREMOVAL,
            DEVICEQUERYREMOVE,
            DEVICEQUERYREMOVEFAILED,
            DEVICEREMOVEPENDING,
            DEVICEREMOVECOMPLETE,
            DEVICECUSTOMEVENT,
            DEVICEINSTANCEENUMERATED,
            DEVICEINSTANCESTARTED,
            DEVICEINSTANCEREMOVED,
            MAX
        }
    }
}