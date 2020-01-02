//using QCloud.CosApi.Common; 
using System;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.CosException;
using COSXML.Model.Service;
using COSXML.Utils;
using EF.BLL.Model;
using COSXML.Model.Tag;
using COSXML.Callback;
using System.IO;

namespace TencentCos
{
    public class TencentCosFun
    {
        public string SecretId;
        public string SecretKey;
        public long DurationSecond;
        public string SetAppid;
        public string SetRegion;
        public int SetConnectionTimeoutMs;
        public int SetReadWriteTimeoutMs;
        public bool IsHttps;
        public TencentCosFun()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretId">云 API 密钥 SecretId</param>
        /// <param name="secretKey">云 API 密钥 SecretKey</param>
        /// <param name="durationSecond">每次请求签名有效时长，单位为秒</param>
        /// <param name="setAppid">设置腾讯云账户的账户标识 APPID</param>
        /// <param name="SetRegion">设置一个默认的存储桶地域</param>
        /// <param name="setConnectionTimeoutMs">设置连接超时时间，单位毫秒，默认45000ms</param>
        /// <param name="setReadWriteTimeoutMs">设置读写超时时间，单位毫秒，默认45000ms</param>
        /// <param name="isHttps">设置默认 HTTPS 请求</param>
        public TencentCosFun(
            string secretId,
            string secretKey,
            long durationSecond,
            string setAppid,
            string SetRegion,
            int setConnectionTimeoutMs =45000,
            int setReadWriteTimeoutMs =45000,
            bool isHttps=true
            )
        {
            this.SecretId=secretId;
            this.SecretKey=secretKey;
            this.DurationSecond=durationSecond;
            this.SetAppid=setAppid;
            this.SetRegion=SetRegion;
            this.SetConnectionTimeoutMs=setConnectionTimeoutMs;
            this.SetReadWriteTimeoutMs=setReadWriteTimeoutMs;
            this.IsHttps=isHttps;
        }
       
        /// <summary>
        /// 查询存储桶列表
        /// </summary>
        public ResultModel<ListAllMyBuckets> GetChucunto()
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
                                  .SetConnectionTimeoutMs(SetConnectionTimeoutMs)  //设置连接超时时间，单位毫秒，默认45000ms
                                  .SetReadWriteTimeoutMs(SetReadWriteTimeoutMs)  //设置读写超时时间，单位毫秒，默认45000ms
                                  .IsHttps(IsHttps)  //设置默认 HTTPS 请求
                                  .SetAppid(SetAppid) //设置腾讯云账户的账户标识 APPID
                                  .SetRegion(SetRegion) //设置一个默认的存储桶地域
                                  .Build();

            string secretId = SecretId;   //云 API 密钥 SecretId
            string secretKey = SecretKey; //云 API 密钥 SecretKey
            long durationSecond = DurationSecond;          //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
              secretKey, durationSecond);

            CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);
            ResultModel<ListAllMyBuckets> result = new ResultModel<ListAllMyBuckets>();
            try
            {
                GetServiceRequest request = new GetServiceRequest();
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetServiceResult cosresult = cosXml.GetService(request);

                result.date = cosresult.listAllMyBuckets;
                result.success = true;
                result.code = 200;
                result.message = cosresult.GetResultInfo();
                //请求成功
                return result;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                result.message = clientEx.Message;
                result.success = false;
                return result;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                result.message = serverEx.Message;
                result.success = false;
                return result;
            }
        }

        /// <summary>
        /// 查询指定路径下面的对象
        /// </summary>
        /// <param name="tEST"></param>
        /// <returns></returns>
        public ResultModel<ListBucket> GetBucketResult(string tEST) 
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
                  .SetConnectionTimeoutMs(SetConnectionTimeoutMs)  //设置连接超时时间，单位毫秒，默认45000ms
                  .SetReadWriteTimeoutMs(SetReadWriteTimeoutMs)  //设置读写超时时间，单位毫秒，默认45000ms
                  .IsHttps(IsHttps)  //设置默认 HTTPS 请求
                  .SetAppid(SetAppid) //设置腾讯云账户的账户标识 APPID
                  .SetRegion(SetRegion) //设置一个默认的存储桶地域
                  .Build();
            string secretId = SecretId;   //云 API 密钥 SecretId
            string secretKey = SecretKey; //云 API 密钥 SecretKey
            long durationSecond = 600;          //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
              secretKey, durationSecond);

            CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);
            ResultModel<ListBucket> resu = new ResultModel<ListBucket>();
            try
            {
                string bucket = "yuanguhl"; //格式：BucketName-APPID
                GetBucketRequest request = new GetBucketRequest(bucket);
                request.Region = "ap-beijing";
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //获取 a/ 下的对象
                if (!string.IsNullOrEmpty(tEST))
                {
                    request.SetPrefix(tEST);
                }
                //执行请求
                GetBucketResult result = cosXml.GetBucket(request);
                //请求成功
                resu.date = result.listBucket;
                resu.success = true;
                return resu;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                resu.success = false;
                resu.message = clientEx.InnerException.ToString();
                return resu;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                resu.success = false;
                resu.message = serverEx.InnerException.ToString();
                return resu;
            }
        }



        public void GetFileDow( string keyw,string name, OnProgressCallback Dwjindu,string path)
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
                                  .SetConnectionTimeoutMs(SetConnectionTimeoutMs)  //设置连接超时时间，单位毫秒，默认45000ms
                                  .SetReadWriteTimeoutMs(SetReadWriteTimeoutMs)  //设置读写超时时间，单位毫秒，默认45000ms
                                  .IsHttps(IsHttps)  //设置默认 HTTPS 请求
                                  .SetAppid(SetAppid) //设置腾讯云账户的账户标识 APPID
                                  .SetRegion(SetRegion) //设置一个默认的存储桶地域
                                  .Build();

            string secretId = SecretId;   //云 API 密钥 SecretId
            string secretKey = SecretKey; //云 API 密钥 SecretKey
            long durationSecond = 600;          //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
              secretKey, durationSecond);


            CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);

            try
            {
                string bucket = "yuanguhl"; //存储桶，格式：BucketName-APPID
                string key = keyw; //对象在存储桶中的位置，即称对象键
                string localDir = path;//本地文件夹
                string localFileName = name; //指定本地保存的文件名
                GetObjectRequest request = new GetObjectRequest(bucket, key, localDir, localFileName);
                request.Region = "ap-beijing";

                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(Dwjindu);
                //执行请求
                GetObjectResult result = cosXml.GetObject(request);
                //请求成功
                



            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                throw clientEx;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                throw serverEx;
            }
        }

        public void AddTencentCos(string path, OnProgressCallback postpath,string name)
        {
            CosXmlConfig config = new CosXmlConfig.Builder()
                                  .SetConnectionTimeoutMs(SetConnectionTimeoutMs)  //设置连接超时时间，单位毫秒，默认45000ms
                                  .SetReadWriteTimeoutMs(SetReadWriteTimeoutMs)  //设置读写超时时间，单位毫秒，默认45000ms
                                  .IsHttps(IsHttps)  //设置默认 HTTPS 请求
                                  .SetAppid(SetAppid) //设置腾讯云账户的账户标识 APPID
                                  .SetRegion(SetRegion) //设置一个默认的存储桶地域
                                  .Build();

            string secretId = SecretId;   //云 API 密钥 SecretId
            string secretKey = SecretKey; //云 API 密钥 SecretKey
            long durationSecond = 60000;          //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider qCloudCredentialProvider = new DefaultQCloudCredentialProvider(secretId,
              secretKey, durationSecond);

            CosXml cosXml = new CosXmlServer(config, qCloudCredentialProvider);

            try
            {
                string bucket = "yuanguhl"; //存储桶，格式：BucketName-APPID
                string key = $"{DateTime.Now.ToString("yy")}/Add/"+name; //对象在存储桶中的位置，即称对象键
                string srcPath = path;//本地文件绝对路径
                if (!File.Exists(srcPath))
                {
                    // 如果不存在目标文件，创建一个临时的测试文件
                    File.WriteAllBytes(srcPath, new byte[1024]);
                }
                PostObjectRequest request = new PostObjectRequest(bucket, key, srcPath);
                request.Region = "ap-beijing";
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(postpath);
                //执行请求
                PostObjectResult result = cosXml.PostObject(request);
                //请求成功


            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                throw clientEx;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                throw serverEx;
            }

        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

    }
}
