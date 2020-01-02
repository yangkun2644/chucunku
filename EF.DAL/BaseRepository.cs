using EF.DAL.DAL;
using EF.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{

    public class BaseRepository<T> : BaseClass, IBaseRepository<T> where T : class
    {

        public Model1 dbEF = DbContextFactory.GetCurrentContext();

        //public ELDBEntity dbEF = ELDBEntity();



        public virtual T Add(T entity)

        {

            dbEF.Entry<T>(entity).State = EntityState.Added;

            dbEF.SaveChanges();


            return entity;

        }



        public virtual bool AddOK(T entity)

        {

            dbEF.Entry<T>(entity).State = EntityState.Added;

            return dbEF.SaveChanges() > 0;

        }



        public virtual int Count(Expression<Func<T, bool>> predicate)

        {

            return dbEF.Set<T>().AsNoTracking().Count(predicate);

        }



        public virtual T Update(T entity, bool detach = false)

        {

            dbEF.Set<T>().Attach(entity);

            dbEF.Entry<T>(entity).State = EntityState.Modified;

            dbEF.SaveChanges();

            if (detach)
            {
                ObjectContext oc = ((IObjectContextAdapter)dbEF).ObjectContext;
                oc.Detach(entity);
            }

            return entity;

        }



        public virtual bool UpdateOK(T entity,bool detach = false)

        {

            dbEF.Set<T>().Attach(entity);

            dbEF.Entry<T>(entity).State = EntityState.Modified;

            bool bol = dbEF.SaveChanges() > 0;

            if (detach)
            {
                ObjectContext oc = ((IObjectContextAdapter)dbEF).ObjectContext;
                oc.Detach(entity);
            }

            return bol;

        }



        public virtual bool Delete(T entity)

        {

            dbEF.Set<T>().Attach(entity);

            dbEF.Entry<T>(entity).State = EntityState.Deleted;


            return dbEF.SaveChanges() > 0;

        }



        public virtual bool Exist(Expression<Func<T, bool>> anyLambda)

        {

            return dbEF.Set<T>().AsNoTracking().Any(anyLambda);

        }



        public virtual T Find(Expression<Func<T, bool>> whereLambda)

        {

            T _entity = dbEF.Set<T>().AsNoTracking().FirstOrDefault<T>(whereLambda);

            return _entity;

        }



        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba)

        {

            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            return _list;
        }

        public virtual IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
            else _list = _list.OrderByDescending<T, S>(orderLamdba);
            return _list;
        }



        public virtual IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }

        //public virtual PagedList<T> FindPageList1<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        //{
        //    var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
        //    totalRecord = _list.Count();
        //    PagedList<T> resultList = null;
        //    if (isAsc) resultList = _list.OrderBy<T, S>(orderLamdba).ToPagedList(pageIndex, pageSize);
        //    else resultList = _list.OrderByDescending<T, S>(orderLamdba).ToPagedList(pageIndex, pageSize);
        //    return resultList;
        //}



        public virtual List<T> FindListBySQL<T>(string sql, params object[] parameters)
        {
            var list = dbEF.Database.SqlQuery<T>(sql, parameters).ToList();
            return list;
        }


        public virtual int ExecuteBySQL(string sql, params object[] parameters)
        {
            var q = dbEF.Database.ExecuteSqlCommand(sql, parameters);
            return q;
        }
    }

}
