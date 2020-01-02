using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.BLL.Model
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 成功时具体返回值
        /// </summary>
        public T date { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 具体错误消息
        /// </summary>
        public string message { get; set; }

        public ResultModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateg">值</param>
        /// <param name="success">是否成功（默认成功）</param>
        /// <param name="codeg">错误码（默认0）</param>
        /// <param name="message">具体错误消息（默认空）</param>
        public ResultModel(T dateg,bool successg = true,int codeg=0,string messageg="")
        {
            this.code = codeg;
            this.date = dateg;
            this.success = successg;
            this.code = code;
            this.message = messageg;
        }

    }
}
