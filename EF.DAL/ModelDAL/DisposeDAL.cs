using EF.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL.ModelDAL
{
    public class DisposeDAL : BaseRepository<Dispose>
    {

        /// <summary>
        /// 返回指定配置的数据
        /// </summary>
        /// <param name="Name">配置的名称</param>
        /// <returns>返回Dispose实体</returns>
        public Dispose GetName(string Name) {
            return Find(x=>x.DisposeName==Name);
        }
    }
}
