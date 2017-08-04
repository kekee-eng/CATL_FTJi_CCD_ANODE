
namespace Common {
#pragma warning disable 0649

    public class DataGrab {

        //取相相机
        public string Camera;

        //帧数据
        public int Frame;
        public int Encoder;
        public string Timestamp;

        //图像
        public HalconDotNet.HImage Image;

        //数据库接口
        public object[] ToDB() {
            return new object[] {
                Camera, Frame, Encoder, Timestamp,
                UtilSerialization.obj2bytes(Image)
            };
        }
        public static DataGrab FromDB(object[] objs) {
            System.Func<object, object, object> getDef = (obj, def) => obj is System.DBNull ? def : obj;

            return new DataGrab() {
                Camera = (string)getDef(objs[1], ""),
                Frame = (int)getDef(objs[2], 0),
                Encoder = (int)getDef(objs[3], 0),
                Timestamp = (string)getDef(objs[4], ""),
                Image = (HalconDotNet.HImage)getDef(objs[5], null),
            };
        }

        #region 数据库命令
        public static readonly string DB_CreateTable = @"CREATE TABLE IF NOT EXISTS {0} 
(
ID              INTEGER     PRIMARY KEY     AUTOINCREMENT,
Camera          TEXT,
Frame           INTEGER,
Encoder         INTEGER,
Timestamp       TEXT,
Image           BLOB
)";
        public static readonly string DB_Insert = @"INSERT INTO {0} ( Camera, Frame, Encoder, Timestamp, Image ) VALUES (  ?,?,?,?,? ) ";
        public static readonly string DB_Select = @"SELECT * FROM {0} WHERE Frame=""{1}""";


        #endregion

    }
}
