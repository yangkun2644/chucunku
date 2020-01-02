using COSXML;
using COSXML.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TencentCos
{

    /// <summary>
    /// 临时密钥获取
    /// </summary>
    public class MyQCloudCredentialProvider : QCloudCredentialProvider
    {
        public override QCloudCredentials GetQCloudCredentials()
        {
            string secretId = "AKID87r4ZxDRQ1xjKqfbnmZHqKtLfnmV3Ly2"; //密钥 SecretId
            string secretKey = "3qOfIhojY6SZQ1NSvdHtHm17xMVO1RpC"; //密钥 SecretKey
            //密钥有效时间, 精确到秒，例如1546862502;1546863102
            string keyTime = "1546862502;1546863102";
            return new QCloudCredentials(secretId, secretKey, keyTime);
        }

        public override void Refresh()
        {
            //更新密钥信息，密钥过期会自动回调该方法
        }
    }
}
