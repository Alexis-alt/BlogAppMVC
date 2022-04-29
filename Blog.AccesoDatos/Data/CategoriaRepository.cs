using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        
        //La Clase Repository es la clase Padre de todos los Modelos o Repositorios
        //La Clase es generica, donde especificamos que <T> TIENE QUE SER UNA CLASE
        //De ella heredamos métodos CRUD excepto el de Update que de implementa de diferente forma en cada Modelo
        


        private readonly ApplicationDbContext _db;



        //La clase Padre para todos los Repositorios, necesita un parametro (DE TIPO DBContext) el cual la incializamos desde la instanciazión de este modelo o respositorio
        public CategoriaRepository(ApplicationDbContext db):base(db)
        {

            _db = db;

        }

        //El SelectListItem se usa para mostrar data en los DropDown
        //Este metodo retorna una lista de Categorias, las cuales se muestran en un DropDown

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            

                                                //Por cada registro de la Tabla se genera un Elemento de TipoSelectListItem
            return _db.Categoria.Select(i => new SelectListItem()
            {
                //Valor visible
                Text=i.Nombre,
                //Id del nombre
                Value = i.IdCategoria.ToString()
            }
            
            );
        }


        //El Update se coloca directamente en cada Entidad debido que se implementa diferente en cada una
        public void Update(Categoria categoria)
        {
            var registroActualizar = _db.Categoria.FirstOrDefault(s=>s.IdCategoria == categoria.IdCategoria);

            registroActualizar.Nombre = categoria.Nombre;
            registroActualizar.Orden = categoria.Orden;

            _db.SaveChanges();

        }
    }
}
