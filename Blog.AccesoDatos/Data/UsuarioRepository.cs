using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        
        //La Clase Repository es la clpase Padre de todos los Modelos o Repositorios
        //La Clase es generica, donde especificamos que <T> TIENE QUE SER UNA CLASE
        //De ella heredamos métodos CRUD excepto el de Update que de implementa de diferente forma en cada Modelo
        


        private readonly ApplicationDbContext _db;



        //La clase Padre para todos los Repositorios, necesita un parametro (DE TIPO DBContext) el cual la incializamos desde la instanciazión de este modelo o respositorio
        public UsuarioRepository(ApplicationDbContext db):base(db)
        {

            _db = db;

        }






        public void BloquearUsuario(string IdUsuario)
        {
            var user = _db.ApplicationUser.Find(IdUsuario);


                             //A la fecha de hoy le añadimos 100 años y nos da como resultado la fecha de hoy pero 100 años adelante
                             //Es cuando se desbloqueará el usuario
            user.LockoutEnd = DateTime.Now.AddYears(100);

            _db.SaveChanges();

        }

        public void DesbloquearUsuario(string IdUsuario)
        {


            var user = _db.ApplicationUser.Find(IdUsuario);

            //Se desbloquea ahora
            user.LockoutEnd = DateTime.Now;

            _db.SaveChanges();


        }



    }
}
