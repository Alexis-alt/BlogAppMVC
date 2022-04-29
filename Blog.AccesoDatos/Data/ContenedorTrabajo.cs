using Blog.AccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {

        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {

             _db = db;

            //Aqui tenemos que colocar todas las Entidades 
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Slider = new SliderRespository(_db);
            Usuario = new UsuarioRepository(_db);
        }

        
        //Estas propiedades  y el  método Save() se implementan de la Interfaz IContenedorTrabajo
        //Aquí colocamos las propiedades que referencian a las interfaces de cada una de las entidades

        public ICategoriaRepository Categoria { get; private set; }

        public IArticuloRepository Articulo { get; private set; }

        public ISliderRepository Slider { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }



        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
