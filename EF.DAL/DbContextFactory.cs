using EF.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{
    namespace DAL
    {
        public static class DbContextFactory
        {
            public static Model1 GetCurrentContext()
            {
                Model1 _nContext = CallContext.GetData("Model1") as Model1;
                if (_nContext == null)
                {
                    _nContext = new Model1();
                    CallContext.SetData("Model1", _nContext);
                }
                return _nContext;
            }
        }
    }
}
