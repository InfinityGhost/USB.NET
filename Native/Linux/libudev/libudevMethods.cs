using System;
using System.Runtime.InteropServices;

namespace Native.Linux.libudev
{
    public unsafe static class libudevMethods
    {
        const string libudev = "libudev.so";

        #region Library Context
            
        [DllImport(libudev)]
        public static extern void* udev_ref(void* udev);

        [DllImport(libudev)]
        public static extern void* udev_unref(void* udev);

        [DllImport(libudev)]
        public static extern void* udev_new();

        #endregion

        #region Lists
            
        [DllImport(libudev)]
        public static extern void* udev_list_entry_get_next(void* list_entry);

        [DllImport(libudev)]
        public static extern void* udev_list_entry_get_by_name(
            void* list_entry,
            [MarshalAs(UnmanagedType.LPStr)] string name
        );

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_list_entry_get_name(void* list_entry);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_list_entry_get_value(void* list_entry);

        #endregion

        #region Device
            
        [DllImport(libudev)]
        public static extern void* udev_device_ref(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_unref(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_udev(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_new_from_syspath(
            void* udev,
            [MarshalAs(UnmanagedType.LPStr)] string syspath
        );

        [DllImport(libudev)]
        public static extern void* udev_device_new_from_devnum(void* udev, char type, ulong devnum);

        [DllImport(libudev)]
        public static extern void* udev_device_new_from_subsystem_sysname(
            void* udev,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string sysname
        );

        [DllImport(libudev)]
        public static extern void* udev_device_new_from_device_id(
            void* udev,
            [MarshalAs(UnmanagedType.LPStr)] string id
        );

        [DllImport(libudev)]
        public static extern void* udev_device_new_from_environment(void* udev);

        [DllImport(libudev)]
        public static extern void* udev_device_get_parent(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_parent_with_subsystem_devtype(
            void* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string devtype
        );

        #endregion

        #region Device Properties
            
        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devpath(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_subsystem(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devtype(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_syspath(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysname(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysnum(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_devnode(void* udev_device);

        [DllImport(libudev)]
        public static extern int udev_device_get_is_initialized(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_devlinks_list_entry(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_properties_list_entry(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_tags_list_entry(void* udev_device);

        [DllImport(libudev)]
        public static extern void* udev_device_get_sysattr_list_entry(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_property_value(
            void* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string key
        );

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_driver(void* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_devnum(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_action(void* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_seqnum(void* udev_device);

        [DllImport(libudev)]
        public static extern ulong udev_device_get_usec_since_initialized(void* udev_device);

        [DllImport(libudev)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string udev_device_get_sysattr_value(
            void* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string sysattr
        );

        [DllImport(libudev)]
        public static extern int udev_device_set_sysattr_value(
            void* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string sysattr,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_device_has_tag(
            void* udev_device,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );
        
        #endregion

        #region Monitor
            
        [DllImport(libudev)]
        public static extern void* udev_monitor_ref(void* udev_monitor);

        [DllImport(libudev)]
        public static extern void* udev_monitor_unref(void* udev_monitor);

        [DllImport(libudev)]
        public static extern void* udev_monitor_get_udev(void* udev_monitor);

        [DllImport(libudev)]
        public static extern void* udev_monitor_new_from_netlink(
            void* udev,
            [MarshalAs(UnmanagedType.LPStr)] string name
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_enable_receiving(void* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_set_receive_buffer_size(void* udev_monitor, int size);

        [DllImport(libudev)]
        public static extern int udev_monitor_get_fd(void* udev_monitor);

        [DllImport(libudev)]
        public static extern void* udev_monitor_receive_device(void* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_add_match_subsystem_devtype(
            void* udev_monitor,
            [MarshalAs(UnmanagedType.LPStr)] string subsystem,
            [MarshalAs(UnmanagedType.LPStr)] string devtype
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_add_match_tag(
            void* udev_monitor,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_update(void* udev_monitor);

        [DllImport(libudev)]
        public static extern int udev_monitor_filter_remove(void* udev_monitor);

        #endregion
        
        #region Enumeration

        [DllImport(libudev)]
        public static extern void* udev_enumerate_ref(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern void* udev_enumerate_unref(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern void* udev_enumerate_get_udev(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern void* udev_enumerate_new(void* udev);

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_subsystem(
            void* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_nomatch_subsystem(
            void* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_sysattr(
            void* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_nomatch_sysattr(
            void* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_property(
            void* udev_enumerator, 
            [MarshalAs(UnmanagedType.LPStr)] string property, 
            [MarshalAs(UnmanagedType.LPStr)] string value
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_sysname(
            void* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string sysname
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_tag(
            void* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string tag
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_match_is_initialized(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern int udev_enumerate_add_syspath(
            void* udev_enumerator,
            [MarshalAs(UnmanagedType.LPStr)] string syspath
        );

        [DllImport(libudev)]
        public static extern int udev_enumerate_scan_devices(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern int udev_enumerate_scan_subsystems(void* udev_enumerate);

        [DllImport(libudev)]
        public static extern void* udev_enumerate_get_list_entry(void* udev_enumerate);

        #endregion
    }
}