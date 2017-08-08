using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD {

    using System.Diagnostics;
    using System.Management;

    public class UtilPerformance {

        static string processName = Process.GetCurrentProcess().ProcessName;

        static PerformanceCounter _App_DiskRead = new PerformanceCounter("Process", "IO Read Bytes/sec", processName);
        static PerformanceCounter _App_DiskWrite = new PerformanceCounter("Process", "IO Write Bytes/sec", processName);
        static PerformanceCounter _App_MemoryLoad = new PerformanceCounter("Process", "Working Set - Private", processName);
        static PerformanceCounter _App_CpuLoad = new PerformanceCounter("Process", "% Processor Time", processName);

        static Lazy<int> _App_MemoryTotal = new Lazy<int>(() => {

            if(!HalconDotNet.HalconAPI.isWindows)
                return 0;
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();
            searcher.Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[] { "Capacity" });
            ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext()) {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties["Capacity"].Value != null) {
                    try {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                    catch {
                        return 0;
                    }
                }
            }
            return (int)(capacity / 1024 / 1024);

        });

        public static int GetCpuCount() {  return Environment.ProcessorCount; }
        public static double GetCpuLoad() { return _App_CpuLoad.NextValue()/ Environment.ProcessorCount; }
        
        public static int GetMemoryTotal() { return _App_MemoryTotal.Value; }
        public static double GetMemoryLoad() { return _App_MemoryLoad.NextValue() / 1024 / 1024; }

        public static double GetDiskRead() { return _App_DiskRead.NextValue() / 1024 / 1024; }
        public static double GetDiskWrite() { return _App_DiskWrite.NextValue() / 1024 / 1024; }

        public static double GetDiskFree(string Driver) {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (System.IO.DriveInfo drive in drives) {
                if (drive.Name.ToUpper()[0] == Driver.ToUpper()[0]) {
                    var freeSize = drive.TotalFreeSpace / (1024.0 * 1024 * 1024);
                    var totalSize = drive.TotalSize / (1024.0 * 1024 * 1024);
                    return freeSize;
                }
            }

            return -1;
        }
        public static double GetDiskTotal(string Driver) {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (System.IO.DriveInfo drive in drives) {
                if (drive.Name.ToUpper()[0] == Driver.ToUpper()[0]) {
                    var freeSize = drive.TotalFreeSpace / (1024.0 * 1024 * 1024);
                    var totalSize = drive.TotalSize / (1024.0 * 1024 * 1024);
                    return totalSize;
                }
            }

            return -1;
        }
        public static double GetDiskUse(string Driver) {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (System.IO.DriveInfo drive in drives) {
                if (drive.Name.ToUpper()[0] == Driver.ToUpper()[0]) {
                    var freeSize = drive.TotalFreeSpace / (1024.0 * 1024 * 1024);
                    var totalSize = drive.TotalSize / (1024.0 * 1024 * 1024);
                    return totalSize - freeSize;
                }
            }

            return -1;
        }

        public static string GetInformaticon() {
            return string.Format(@"=========================
内存占用     = {0:0.0} M

磁盘读取速度 = {1:0.0} M/s
磁盘写入速度 = {2:0.0} M/s 

硬盘容量     = {3:0.0} G
已使用容量   = {4:0.0} G
剩余容量     = {5:0.0} G

",
    GetMemoryLoad(),

    GetDiskRead(),
    GetDiskWrite(),

    GetDiskTotal("A"),
    GetDiskUse("A"),
    GetDiskFree("A")

    );
        }


        static DateTime _App_StartTime = DateTime.Now;
        public static string GetAppRuntime() {

            var t = DateTime.Now - _App_StartTime;

            int day = t.Days;
            int hour = t.Hours;
            int minute = t.Minutes;
            int second = t.Seconds;

            return string.Format("{0}{1:00}:{2:00}:{3:00}", day == 0 ? "" : string.Format("{0} Day ", day), hour, minute, second);
        }

    }
}
