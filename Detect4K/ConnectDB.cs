using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detect4K {
    class ConnectDB : Common.TemplateDB {

        public void Init() {

            //创建数据表
            write(string.Format(DataGrab.DB_CreateTable, TextGrabInner));
            write(string.Format(DataGrab.DB_CreateTable, TextGrabOuter));


        }

        static readonly string TextGrabInner = "GrabInner";
        static readonly string TextGrabOuter = "GrabOuter";
        
        public void SaveGrabInner(DataGrab data) {
            if (count(TextGrabInner + " WHERE Frame=" + data.Frame) == 0) {
                write(string.Format(DataGrab.DB_Insert, TextGrabInner), data.ToDB());
            }
        }
        public DataGrab GetGrabInner(int frame) {
            var ret = read(string.Format(DataGrab.DB_Select, TextGrabInner, frame));
            if (ret.Count == 0)
                return null;
            return DataGrab.FromDB(ret[0]);
        }

        public void SaveGrabOuter(DataGrab data) {
            write(string.Format(DataGrab.DB_Insert, TextGrabOuter), data.ToDB());
        }
        public DataGrab GetGrabOuter(int frame) {
            var ret = read(string.Format(DataGrab.DB_Select, TextGrabOuter, frame));
            if (ret.Count == 0)
                return null;
            return DataGrab.FromDB(ret[0]);
        }
        
    }
}
