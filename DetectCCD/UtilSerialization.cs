
namespace DetectCCD {
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.Serialization.Formatters.Binary;

    public class UtilSerialization {

        public static byte[] obj2bytes(object obj) {
            if (obj == null)
                return null;

            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream()) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
            }
            return bytes;
        }
        public static object bytes2obj(byte[] bytes) {
            if (bytes == null)
                return null;

            object ret = null;
            using (MemoryStream ms = new MemoryStream(bytes)) {
                BinaryFormatter formatter = new BinaryFormatter();
                ret = formatter.Deserialize(ms);
            }
            return ret;
        }
        static void bytes2file(byte[] bytes, string path) {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
        static byte[] file2bytes(string path) {
            byte[] ret = null;
            using (MemoryStream outStream = new MemoryStream()) {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    fileStream.CopyTo(outStream);
                    ret = outStream.ToArray();
                }
            }
            return ret;
        }
        static byte[] zip(byte[] bytes) {
            if (bytes == null)
                return null;

            byte[] ret = null;
            using (MemoryStream outStream = new MemoryStream()) {
                using (GZipStream zipStream = new GZipStream(outStream, CompressionMode.Compress, true)) {
                    zipStream.Write(bytes, 0, bytes.Length);
                    zipStream.Dispose();
                    ret = outStream.ToArray();
                }
            }
            return ret;
        }
        static byte[] unzip(byte[] bytes) {
            if (bytes == null)
                return null;

            byte[] ret = null;
            using (MemoryStream inputStream = new MemoryStream(bytes)) {
                using (MemoryStream outStream = new MemoryStream()) {
                    using (GZipStream zipStream = new GZipStream(inputStream, CompressionMode.Decompress)) {
                        zipStream.CopyTo(outStream);
                        zipStream.Dispose();
                        ret = outStream.ToArray();
                    }
                }
            }
            return ret;
        }

    }

}
