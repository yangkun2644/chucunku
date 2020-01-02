using EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.BLL
{

    //
    public class BaseRepositoryBLL<T> : BaseClass, IBaseRepositoryBLL<T> where T : class

    {
        BaseRepository<T> obj = new BaseRepository<T>();
        public virtual T Add(T entity)
        {
            return obj.Add(entity);
        }



        public virtual bool AddOK(T entity)
        {
            return obj.AddOK(entity);
        }



        public virtual int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return obj.Count(predicate);
        }



        public virtual T Update(T entity)
        {
            return obj.Update(entity);
        }



        public virtual bool UpdateOK(T entity, bool v=false)
        {
            return obj.UpdateOK(entity,v);
        }



        public virtual bool Delete(T entity)
        {
            return obj.Delete(entity);
        }



        public virtual bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> anyLambda)
        {
            return obj.Exist(anyLambda);
        }



        public virtual T Find(Expression<Func<T, bool>> whereLambda)
        {
            return obj.Find(whereLambda);
        }



        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba)
        {
            var _list = obj.FindList(whereLamdba);
            return _list;
        }



        public virtual IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, System.Linq.Expressions.Expression<Func<T, S>> orderLamdba)
        {
            return obj.FindList<S>(whereLamdba, isAsc, orderLamdba);
        }



        public virtual IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)

        {
            return obj.FindPageList<S>(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);
        }


        //public virtual PagedList<T> FindPageList1<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        //{
        //    return obj.FindPageList1<S>(pageIndex, pageSize, out totalRecord, whereLamdba, isAsc, orderLamdba);
        //}


        public virtual List<T> FindListBySQL<T>(string sql, params object[] parameters)
        {
            return obj.FindListBySQL<T>(sql, parameters);
        }


        public virtual int ExecuteBySQL(string sql, params object[] parameters)

        {
            return obj.ExecuteBySQL(sql, parameters);
        }
    }
}
