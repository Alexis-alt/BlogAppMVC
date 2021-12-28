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
        //Parametros: delegado que recibe un Gnerico y regresa un boolean; Por defecto estan en null
        //Delegado que recibe un conjunto de elementos de tipo generico y los devuelve ordenados = Por defecto nulll
        //String para incluir propiedades, por defecto null


        //El IQueryable permite realizar consultas de filtrado que ejecutan en BD como un SPR 
        //Permite performace debido a que las consultas se ejecutan en el servidor hasta colocar un toList o un Foreach


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
