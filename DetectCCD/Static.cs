using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;
using System.IO;
using System.Threading;

namespace DetectCCD {

   public class Static {

        public static string Root { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        
        public static string FolderCfg { get { return Root + "../Config/"; } }
        public static string FolderCfgBackup { get { return "D:\\Backup\\"; } }
        public static string FolderRecord {
            get {
                //保存路径结构修改为：日期 ->品种 ->膜卷号 ->分类 ->大图 /小图
                Func<string, string> fixPath = (s) => s == "" ? "[UNKNOW]" : s;
                return string.Format("../Record/{0}/{1}/{2}",
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    fixPath(Static.Recipe.RecipeName),
                    fixPath(Static.App.RollName));
            }
        }
        public static string FolderTemp { get { return Root + "../Temp/"; } }

        public static string PathCfgApp { get { return FolderCfg + "cfg_app.xml"; } }
        public static string PathCfgRecipe { get { return FolderCfg + "cfg_param.xml"; } }
        public static string PathCfgTiebiao { get { return FolderCfg + "cfg_tiebiao.xml"; } }
        public static string PathImageProcess { get { return FolderCfg + "image_process.hdev"; } }
        public static string PathCamera { get { return FolderCfg + "card.ccf"; } }

        public static string PathCfgAppBackup { get { return FolderCfgBackup + "cfg_app.xml"; } }
        public static string PathImageProcessBackup { get { return FolderCfgBackup + "image_process.hdev"; } }
        public static string PathCfgTiebiaoBackup { get { return FolderCfgBackup + "cfg_tiebiao.xml"; } }
        public static string PathCfgRecipeBackup { get { return FolderCfgBackup + "cfg_param.xml"; } }
        public static string PathCameraBackup { get { return FolderCfgBackup + "card.ccf"; } }


        public static CfgApp App;
        public static CfgRecipe Recipe;
        public static CfgTiebiao Tiebiao;
        
        public static void Init() {
            
            //
            App = new CfgApp(PathCfgApp);
            Recipe = new CfgRecipe(PathCfgRecipe);
            Tiebiao = new CfgTiebiao(PathCfgTiebiao);

            //
            App.Save();
            Recipe.Save();
            Tiebiao.Save();

            //
            App.SelectUserId = 0;
        }
        public static void Uninit() {

            App.Save();
            Recipe.Save();

        }

        public static bool CompareFile(string p_1, string p_2)
        {
            try
            {
                //计算第一个文件的哈希值
                var hash = System.Security.Cryptography.HashAlgorithm.Create();
                var stream_1 = new System.IO.FileStream(p_1, System.IO.FileMode.Open);
                byte[] hashByte_1 = hash.ComputeHash(stream_1);
                stream_1.Close();
                //计算第二个文件的哈希值
                var stream_2 = new System.IO.FileStream(p_2, System.IO.FileMode.Open);
                byte[] hashByte_2 = hash.ComputeHash(stream_2);
                stream_2.Close();

                //比较两个哈希值
                if (BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2))
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }


        }

        public static bool CopyDirAll(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {

                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        if (string.Equals(System.IO.Path.GetFileName(file), "cfg_param.xml") || string.Equals(System.IO.Path.GetFileName(file), "cfg_app.xml") || string.Equals(System.IO.Path.GetFileName(file), "cfg_tiebiao.xml"))
                        {
                            System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                        }

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
