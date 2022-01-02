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
        
        //La Clase Repository es la clpase Padre de todos los Modelos o Repositorios
        //La Clase es generica, donde especificamos que <T> TIENE QUE SER UNA CLASE
        //De ella heredamos métodos CRUD excepto el de Update que de implementa de diferente forma en cada Modelo
        


        private readonly ApplicationDbContext _db;



        //La clase Padre para todos los Repositorios, necesita un parametro (DE TIPO DBContext) el cual la incializamos desde la instanciazión de este modelo o respositorio
        public CategoriaRepository(ApplicationDbContext db):base(db)
        {

            _db = db;

        }


        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem()
            {
                Text=i.Nombre,
                Value = i.IdCategoria.ToString()
            }
            
            );
        }

        public void Update(Categoria categoria)
        {
            var registroActualizar = _db.Categoria.FirstOrDefault(s=>s.IdCategoria == categoria.IdCategoria);

            registroActualizar.Nombre = categoria.Nombre;
            registroActualizar.Orden = categoria.Orden;

            _db.SaveChanges();

        }
    }
}
