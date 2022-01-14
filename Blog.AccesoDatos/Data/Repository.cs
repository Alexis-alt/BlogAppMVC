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
        //Esta es la Clase Padre de todas las entidades que hacen uso de los métodos genericos que contiene esta clase
        //Implementa de la interfaz IReposiroy de tipo Generico
        

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




        //Los siguienes dos métodos contienen como parametros delegados del tipo Func "Con los cuales indicamos que recibirá una función que recibe un parametro generico y regresa un valor bool"
        //Dichos delegados se implementan dentro del método
        //Se inicializan como null los parametros ya que en algunas ocasiones pueden necesitarse y en otras omitirse

        public T FirstOrDefaul(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {


            
            //Colección de datos //Tabla o Entidad
            IQueryable<T> query = dbSet;

            //Cuando si se envia como parametro una función/delegado para filtrar.
            if (filter != null)
            {
                                   //Aquí se aplica el filtro IMPLEMENTANDOSE EL DELEGADO
                query = query.Where(filter);

            }


            //Cuando se incluyen propiedades

            if (includeProperties != null)
            {
                //Se divide la candena que se recibe como parametro y la cual contiene las Propiedades que se quieren añadir a la consulta
                //Las propiedades que se reciben se separan por comas y se elminan los espacios
                //El array resultante es un ARRAY DE PROPIEDADES las cuales se van iterando y añadiendo a la query;
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    /*El método include() que se aplica a un DbSet, permite poder incluir
                     * en los resultados de la consulta propiedades
                     * que referencian a una propiedad de otra de otra tabla 
                     * 
                     * Es decir llaves foraneás
                     * 
                     * Se puede implementar de 2 formas 
                     * 
                     * DbSet.include(registroQueSeObtiene => registroQueSeObtiene.NombreDeLaPropiedaIncluir)
                     *  
                     * DbSet.include("NombreDeLaPropiedaIncluir")
                     * 
                     */





                    query = query.Include(includeProperty);

                }

            }


            //Se retorna unicamente el primer elemento de toda la colección de datos obtenida
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


            //Se retorna una lista la cual sabemos que hereda de IEnumerable
            //En este caso es hasta aqui donde se manda la consulta al motor de BD, debido a que estamos usando IQueriable el cual primero adjunta todas las conusltas y se ejecuta hasta que se Iteré la colección obtenida o se haga un ToList() 
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
