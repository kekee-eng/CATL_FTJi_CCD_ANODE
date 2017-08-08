
namespace DetectCCD {
    using System.Runtime.InteropServices;

    public class ENC7480 {
        //PCI Board Open/Close
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Enc7480_Init();
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Close();

        //Encoder counts config
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Get_Encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Enc7480_Get_Encoder(ushort axis);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Set_Encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Set_Encoder(ushort axis, int value);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Get_LatchValue", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int Enc7480_Get_LatchValue(ushort axis);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Count_Config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Count_Config(ushort axis, ushort mode);

        //I/O Read or Write
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Write_OutBit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Write_OutBit(ushort bitno, int Off_On);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Write_OutPort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Write_OutPort(ushort cardno, uint value);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Read_OutPort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Enc7480_Read_OutPort(ushort cardno);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Read_InPort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Enc7480_Read_InPort(ushort cardno);

        //Trigger Logic Setup
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Set_Triger_Logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Set_Triger_Logic(ushort logic);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Set_EZ_Logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Set_EZ_Logic(ushort axis, ushort enable, ushort logic);

        //Latch and Flag Setup
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Read_Latch_Status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint Enc7480_Read_Latch_Status(ushort cardno);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Reset_Latch_Flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Reset_Latch_Flag(ushort cardno);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Reset_Cls_Flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Reset_Cls_Flag(ushort cardno);
        [DllImport("ENC7480.dll", EntryPoint = "Enc7480_Led_Logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void Enc7480_Led_Logic(ushort cardno, ushort Logic);

    }
}