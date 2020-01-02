using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TencentCos.Model
{
    public class Contents
    {
        //
        // 摘要:
        //     文件的 eTag
        /// <summary>
        /// 文件的 eTag
        /// </summary>
        public string eTag { get; set; }
        //
        // 摘要:
        //     Object 的 Key
        /// <summary>
        /// Object 的 Key
        /// </summary>
        public string key { get; set; }
        //
        // 摘要:
        //     Object 最后被修改时间
        /// <summary>
        /// Object 最后被修改时间
        /// </summary>
        public string lastModified { get; set; }
        //
        // 摘要:
        //     Bucket 持有者信息 COSXML.Model.Tag.ListBucket.Owner
        /// <summary>
        /// Bucket 持有者信息 COSXML.Model.Tag.ListBucket.Owner
        /// </summary>
        public Owner owner { get; set; }
        //
        // 摘要:
        //     文件大小，单位是 Byte
        /// <summary>
        /// 文件大小，单位是 Byte
        /// </summary>
        public float size { get; set; }
        //
        // 摘要:
        //     Object 的存储级别，枚举值：STANDARD，STANDARD_IA，ARCHIVE COSXML.Common.CosStorageClass
        /// <summary>
        /// Object 的存储级别，枚举值：STANDARD，STANDARD_IA，ARCHIVE COSXML.Common.CosStorageClass
        /// </summary>
        public string storageClass { get; set; }


    }
}
