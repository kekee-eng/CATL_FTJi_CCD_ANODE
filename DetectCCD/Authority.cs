using Microsoft.Win32;
using System;
using Aladdin.HASP;

namespace DetectCCD
{
    /// <summary> 授权管理 </summary>
    class AuthManage
    {
        /// <summary> 检测硬件加密狗 </summary>
        public static void CheckKey()
        {

#if !DEBUG
            string vendorCode =
            "D8CPhYXeRxY72GGDefe03jBMsMqlT+bYl6Pxtl59KvvWV5+fitH9FqwsLQdyXoCrUlpmoH8RDD8rD8q9" +
            "a0GJ3WSgAFVC2UTfNkurwha1eicVIQMHmoALc1WWNyxgp7SLtxNPmUsX/Aisvj78rscGpHsU04QqPXi/" +
            "LtlbVzFtr2nPF56grCnxQo3ACaTjwIoGHPOCKISFeKS/RBXi/ykKw1Szk6JPHQeKH81hkKDPrkru2Y+8" +
            "8ZwDbaJfOV5cdhx4CmM6f2Qx2anQhXy3P+Bh4fDerqzGjoFRCVhPxg9OVxTRIhpzGqmuUtyU1FsYtAP8" +
            "oZ090yKQNZsK35qNPSmEgR67h2lZjEZOcoqrWd+vG9jCIcGCE1RTDgcwfNjpFQnaO6nomkQd+XcE8pIe" +
            "AgGbEvMaCeJ6v4GsDl2AWnm13IxMgUvcjRZRGH5ji3i58+34AWrAupS8VT91yHKEAasIgvQUBtDNNfNk" +
            "lnpTzj6oRFkn6hhWAGjj4olpkpT+t7B7ZdOmpAlk/a6aPaKUoOgTqGBOUOzRq3OUEa/upuItQeaXCFz7" +
            "RFP7oZ/jWWqmqe7vj4YMh+ka21IYD0KKGKtIySLbfgAY3s0XQVLKtghb7b/WU37bu71a0IkzfS9uTEJD" +
            "VwXFMSVmFZZKQqehy/y0iPxWT4uvSdEB4vrTYl48cPfcp3aWmJOAABI+1U51e3iyzKgzmLKXSItUdMZQ" +
            "bO3NT+qy4dQQmqMhK2xaRSUnm4d2i9javiJS8xzM0/mirW2R613jydbXyk5b4cNzamY7Ff9ncux6Dhcz" +
            "wXXq6hprGvDLKRe+OIlRIRL+6RdzTf0hjRT/fa/sq+B/449+ow0ROw+Ab6ytCl1UHR+F1LcrBkuhh057" +
            "MlJE4SZiJp4RQE1LSGmP61LxeASYDShQsPiKnC3rWpBlzMNHvEHf9Dse+E0=";

            Hasp hasp = new Hasp(HaspFeature.FromFeature(1001));
            HaspStatus status = hasp.Login(vendorCode);
            hasp.Logout();

            if (status != HaspStatus.StatusOk) {
                DevExpress.XtraEditors.XtraMessageBox.Show("未插入UsbKey：" + status.ToString());
                Environment.Exit(0);
            }
#endif
        }
    }
}
