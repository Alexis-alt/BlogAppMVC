using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {


        public void Update(Articulo articulo);


    }
}
