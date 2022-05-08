using Blog.Models;
using Blog.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {

        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;



        public InicializadorDB(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole>  roleManager)
        {
            _db = db;

            _userManager = userManager;

            _roleManager = roleManager;



        }

        public void Inicializador()
        {
            try
            {

                if (_db.Database.GetPendingMigrations().Count() > 0)
                {

                    _db.Database.Migrate();


                }



            }
            catch (Exception)
            {

               
            }

            //Si ya hay algún Rol con el nombre especificado se regresa de aqui y ya no continua el flujo del método Inicializar()
            if (_db.Roles.Any(rol => rol.Name == Constants.Admin)) return;


            _roleManager.CreateAsync(new IdentityRole(Constants.Admin)).GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(Constants.User)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser() { 
            
                UserName = "admin@mail.com",
                Email = "admin@mail.com",
                EmailConfirmed = true,
                Nombre = "Alexis"



            },"Admin123/").GetAwaiter().GetResult();


            ApplicationUser usuario = _db.ApplicationUser.Where(u => u.Email == "admin@mail.com").FirstOrDefault();


            _userManager.AddToRoleAsync(usuario,Constants.Admin).GetAwaiter().GetResult();

        }
    }
}
