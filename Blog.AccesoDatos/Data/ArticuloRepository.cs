using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class ArticuloRepository:Repository<Articulo>,IArticuloRepository
    {



        private readonly ApplicationDbContext _db;


        public ArticuloRepository(ApplicationDbContext db):base(db)
        {

            _db = db;

        }

        public void Update(Articulo articulo)
        {
            var registroActualizar = _db.Articulo.FirstOrDefault(s => s.IdArticulo == articulo.IdArticulo);
            registroActualizar.Nombre = articulo.Nombre;
            registroActualizar.Descripcion = articulo.Descripcion;
            registroActualizar.UrlImagen = articulo.UrlImagen;
            registroActualizar.CategoriaId = articulo.CategoriaId;

            //Se guardará desde el controller
            //_db.SaveChanges();






        }
    }
}
