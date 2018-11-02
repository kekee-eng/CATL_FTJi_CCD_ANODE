using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectCCD
{
    [Serializable]
    public class DataPlcAlarmStop
    {
        public bool Enable = false;
        public bool IsAlarm =false;
        public bool IsStop =false;
        public string Message ="";
    }
}
