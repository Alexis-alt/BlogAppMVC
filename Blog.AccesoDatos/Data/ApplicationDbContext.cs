using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //Tener presente que para crear la bd con code first es necesario  crear el contexto de la aplicacion de forma manual
        //Tener instalados los paquetes necesarios en el proyecto donde se enceuntra el contexto
        //Haber referenciado las Modelos o Tablas en los DbSet<>


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Tablas o Entidades de la BD

       public DbSet<Categoria> Categoria { get; set; }

       public DbSet<Articulo> Articulo { get; set; }

       public DbSet<Slider> Slider { get; set; }

    }
}
