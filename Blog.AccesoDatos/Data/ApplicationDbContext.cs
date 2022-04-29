using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data
{

    /*
     * Instalamos los paquetes EntityFrameworkCore
     * Instalamos los paquetes EntityFrameworkCore.SqlServer
     * Para poder usar las clases de los paquetes
     * 
     * Instalamos los paquetes EntityFrameworkCore.Tools 
     * En el proyecto web donde esta la clase Startup para ejecutar los comandos y que se guarde la configuracion de conexión en lo servicios
     * la cual se genera despues de hacer el primer update-database
     * 
     *  Instalamos los paquetes Microsoft.Extensions.Identity
     *  podemos heredar de aqui y ya viene heredando de la clase dbContext junto con los Models que construyen las tablas de Identity
     * 
     * 
    */




    public class ApplicationDbContext : IdentityDbContext
    {
        //Tener presente que para crear la bd con code first es necesario  crear el contexto de la aplicacion de forma manual
        //Tener instalados los paquetes necesarios en el proyecto donde se enceuntra el contexto
        //Haber referenciado las Modelos o Tablas en los DbSet<>


                                    //Todos los Contextos reciben un parametro de este tipo
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Tablas o Entidades de la BD

       public DbSet<Categoria> Categoria { get; set; }

       public DbSet<Articulo> Articulo { get; set; }

       public DbSet<Slider> Slider { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
