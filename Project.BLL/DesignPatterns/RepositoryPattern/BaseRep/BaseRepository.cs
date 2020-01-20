using Project.BLL.DesignPatterns.RepositoryPattern.IntRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.BaseRep
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        public MyContext db;

        public BaseRepository()
        {
            db = DBTool.DBInstance;
        }

        public virtual void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                return;
            }
           
           
        }

        

        //Listeleme Metotları

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != DataStatus.Deleted);
        }

        public List<T> GetUpdates()
        {
            return Where(x => x.Status == DataStatus.Updated);
        }

        public List<T> GetPassive()
        {
            return Where(x => x.Status == DataStatus.Deleted);
        }


        //Ekeleme, Silme, Güncelleme Metotları

        public virtual void Add(T item)
        {
            db.Set<T>().Add(item);
            Save();
        }

        public virtual void Update(T item)
        {
            item.Status = DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            T toBeUpdated = GetByID(item.ID);
            db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public void Delete(T item)
        {
            item.DeletedDate = DateTime.Now;
            item.Status = DataStatus.Deleted;
            Save();
        }

        public void SpecialDelete(T item)
        {
            db.Set<T>().Remove(item);
            Save();
        }


        //Sorgu Metotları

        public T GetByID(int id)
        {
            return db.Set<T>().Find(id);
        }

        public virtual List<T> Where(Expression<Func<T, bool>> exp)
        {
            try
            {
                return db.Set<T>().Where(exp).ToList();
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
            
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().FirstOrDefault(exp);
        }

        public object Select(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Select(exp).ToList();
        }

    }
}
