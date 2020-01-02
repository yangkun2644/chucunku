using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TencentCos.Model
{
    /// <summary>
    /// 保存 GET Service 结果的所有信息
    /// </summary>
    public class ListAllMyBucketsResult
    {
        /// <summary>
        /// 存储桶持有者信息
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// 存储桶列表
        /// </summary>
        public Buckets Buckets { get; set; }
    }
    /// <summary>
    /// 内容
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// 存储桶持有者的完整 ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 存储桶持有者的名字
        /// </summary>
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// 存储桶lbiao
    /// </summary>
    public class Buckets
    {
        public List<Bucket> Bucket { get; set; }
    }

    /// <summary>
    /// 存储桶
    /// </summary>
    public class Bucket
    {
        /// <summary>
        /// 存储桶的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 存储桶所在地域
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 存储桶的创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
