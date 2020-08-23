using System.Runtime.InteropServices;

namespace Native.Linux.libudev
{
    public unsafe static class libudevMethods
    {
        const string libudev = "libudev.so";

        #region Library Context
            
        [DllImport(libudev)]
        public static extern udev* udev_ref(udev* udev);

        [DllImport(libudev)]
        public static extern udev* udev_unref(udev* udev);

        [DllImport(libudev)]
        public static extern udev* udev_new();

        #endregion

        #region Lists
            
        [DllImport(libudev)]
        public static extern udev_list_entry* udev_list_entry_get_next(udev_list_entry* list_entry);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_list_entry_get_by_name(
            udev_list_entry* list_entry,
            [MarshalAs(UnmanagedType.LPStr)] string name
        );

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_list_entry_get_name(udev_list_entry* list_entry);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_list_entry_get_value(udev_list_entry* list_entry);

        #endregion

        #region Device
            
        [DllImport(libudev)]
        public static extern udev_device* udev_device_ref(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_device* udev_device_unref(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev* udev_device_get_udev(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_device* udev_device_new_from_syspath(
            udev* udev,
            [MarshalAs(UnmanagedType.LPStr)] string syspath
        );

        [DllImport(libudev)]
        public static extern udev_device* udev_device_new_from_devnum(udev* udev, char type, ulong devnum);

        [DllImport(libudev)]
        public static extern udev_device* udev_device_new_from_subsystem_sysname(
            udev* udev,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string sysname
        );

        [DllImport(libudev)]
        public static extern udev_device* udev_device_new_from_device_id(
            udev* udev,
            [MarshalAs(UnmanagedType.LPStr)] string id
        );

        [DllImport(libudev)]
        public static extern udev_device* udev_device_new_from_environment(udev* udev);

        [DllImport(libudev)]
        public static extern udev_device* udev_device_get_parent(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_device* udev_device_get_parent_with_subsystem_devtype(
            udev_device* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string devtype
        );

        #endregion

        #region Device Properties
            
        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devpath(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_subsystem(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devtype(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_syspath(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysname(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysnum(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devnode(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern int udev_device_get_is_initialized(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_device_get_devlinks_list_entry(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_device_get_properties_list_entry(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_device_get_tags_list_entry(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_device_get_sysattr_list_entry(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_property_value(
            udev_device* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string key
        );

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_driver(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_devnum(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_action(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_seqnum(udev_device* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_usec_since_initialized(udev_device* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysattr_value(
            udev_device* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string sysattr
        );

        [DllImport(libudev)]
        public static extern int udev_device_set_sysattr_value(
            udev_device* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string sysattr,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_device_has_tag(
            udev_device* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );
        
        #endregion

        #region Monitor
            
        [DllImport(libudev)]
        public static extern udev_monitor* udev_monitor_ref(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern udev_monitor* udev_monitor_unref(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern udev* udev_monitor_get_udev(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern udev_monitor* udev_monitor_new_from_netlink(
            udev* udev,
            [MarshalAs(UnmanagedType.LPStr)] string name
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_enable_receiving(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_set_receive_buffer_size(udev_monitor* udev_monitor, int size);

        [DllImport(libudev)]
        public static extern int udev_monitor_get_fd(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern udev_device* udev_monitor_receive_device(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_add_match_subsystem_devtype(
            udev_monitor* udev_monitor,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string devtype
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_add_match_tag(
            udev_monitor* udev_monitor,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_update(udev_monitor* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_remove(udev_monitor* udev_monitor);

        #endregion
        
        #region Enumeration

        [DllImport(libudev)]
        public static extern udev_enumerate* udev_enumerate_ref(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern udev_enumerate* udev_enumerate_unref(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern udev* udev_enumerate_get_udev(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern udev_enumerate* udev_enumerate_new(udev* udev);

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_subsystem(
            udev_enumerate* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_nomatch_subsystem(
            udev_enumerate* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_sysattr(
            udev_enumerate* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_nomatch_sysattr(
            udev_enumerate* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_property(
            udev_enumerate* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_sysname(
            udev_enumerate* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string sysname
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_tag(
            udev_enumerate* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_is_initialized(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_syspath(
            udev_enumerate* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string syspath
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_scan_devices(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern int udev_enumerate_scan_subsystems(udev_enumerate* udev_enumerate);

        [DllImport(libudev)]
        public static extern udev_list_entry* udev_enumerate_get_list_entry(udev_enumerate* udev_enumerate);

        #endregion
    }
}