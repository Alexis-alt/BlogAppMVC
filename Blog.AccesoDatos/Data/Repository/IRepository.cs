using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    //Indicamos que el generico tienen que ser unicamente clases

    public interface IRepository<T> where T : class
    {
        //Métodos CRUD comunes en todas las clases
        //Unicamente excluimos el método Update debido a que en cada Modelo pueden variar los atributos que se modificaran en la BD


        //Obtener por ID
        T Get(int id);



        //Obtener todos con o sin parametros de busqueda 
        IEnumerable<T> GetAll(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = null
            );


        //Obtener primero de una lista con y sin filtrado
        T FirstOrDefaul(
             Expression<Func<T, bool>> filter = null,
             string includeProperties = null

            );


        //Añadir registro
        void Add(T entity);
       
        //Remover por id
        void Remove(int id);

        //Remover por entidad 
        void Remove(T entity);


    }
}
