using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBOnce {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine();
            Console.WriteLine("--useage----------------");
            Console.WriteLine("TestDBOnce [DB] [InnerGrab/OuterGrab] [path] [frame]");
            Console.WriteLine("TestDBOnce [DB] [InnerGrab/OuterGrab] [path] [frame1] [frame2]");
            Console.WriteLine();

            if (args.Length ==0 )
                return;

            string dbpath = args[0];
            string dbtable = args[1];

            TemplateDB db = new TemplateDB();
            db.Open(dbpath);

            DataGrab.GrabDB grab = new DataGrab.GrabDB(db, dbtable);
            grab.Count = grab.QueryCount;

            Console.WriteLine("--db info----------------");
            Console.WriteLine("Min: " + grab.Min);
            Console.WriteLine("Max: " + grab.Max);
            Console.WriteLine("Count: " + grab.Count);
            Console.WriteLine();

            int k;
            if (args.Length == 4 && int.TryParse(args[2], out k)) {
                grab[k].Image.WriteImage("bmp", 0, args[3]);
            }
        }
    }
}
