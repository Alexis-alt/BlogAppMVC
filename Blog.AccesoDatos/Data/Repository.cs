using Blog.AccesoDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext Context;

        internal DbSet<T> dbSet;



        public Repository(DbContext _Context)
        {

            Context = _Context;

            this.dbSet = _Context.Set<T>();




        }


        public void Add(T entity)
        {
            dbSet.Add(entity);
        }



        public T Get(int id)
        {

            return dbSet.Find(id);
        }


        public T FirstOrDefaul(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {

            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            if (includeProperties != null)
            {

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty);

                }

            }


            return query.FirstOrDefault();



        }


        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {



            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            if (includeProperties != null)
            {

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty);

                }

            }


            if (orderBy != null)
            {

                return orderBy(query).ToList();

            }


            return query.ToList();


        }




        public void Remove(int id)
        {
            var entityToRemove = dbSet.Find(id);

            Remove(entityToRemove);
            
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
