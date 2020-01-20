using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.IntRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        // Listeleme metotları

        List<T> GetAll();

        List<T> GetActives();

        List<T> GetUpdates();

        List<T> GetPassive();


        // Ekleme, Silme, Güncelleme Metotları

        void Add(T item);

        void Update(T item);

        void Delete(T item);

        void SpecialDelete(T item);


        //Sorgu Metotları

        List<T> Where(Expression<Func<T, bool>> exp);

        bool Any(Expression<Func<T, bool>> exp);

        T FirstOrDefault(Expression<Func<T, bool>> exp);

        object Select(Expression<Func<T, bool>> exp);

        T GetByID(int id);
    }
}
