using System;
using EF.BLL.Model;
using EF.DAL.EFModel;
using EF.DAL.ModelDAL;
using System.Collections.Generic;

namespace EF.BLL.ModelBLL
{
    public class DisposeBLL:BaseRepositoryBLL<Dispose>
    {
        DisposeDAL dal = new DisposeDAL();

        /// <summary>
        /// 查询公告
        /// </summary>
        /// <returns></returns>
        public ResultModel<Dispose> GetNotice()
        {
            ResultModel<Dispose> result = new ResultModel<Dispose>()
            {
                date = dal.GetName("Notice")
            };
            return result;
        }

        /// <summary>
        /// 查询指定配置
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ResultModel<Dispose> GetName(string Name)
        {
            ResultModel<Dispose> result = new ResultModel<Dispose>()
            {
                date = dal.GetName(Name)
            };
            return result;
        }


        
    }
}
