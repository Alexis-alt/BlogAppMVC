
using Blog.AccesoDatos.Data;
using Blog.AccesoDatos.Data.Inicializador;
using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Blog.Utilidades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Agregado por EntityFramework para la craecion de la BD
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));



            //Maneja los servicios de autorización
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //Tokens para cambiar email, resetear el password o cambiar número de teléfono

                .AddDefaultTokenProviders();


            //Hacemos la inyección de la Interfaz que nos dejará usar en el constructor diferentes clases que implementen dicha interfaz
            //Ejemplo es que aqui todas las Entidades heredan de la clase *ContenedorTrabajo*, que es la que implementa la interfaz que se inyecta
            //Esto permitira mandar como parametro al contructor de Controller donde se inyecta, multiples clases, simpre y cuando implementen directa o indirectamente la  Interfaz especificada
            services.AddScoped<IContenedorTrabajo,ContenedorTrabajo>();


            services.AddScoped<IInicializadorDB, InicializadorDB>();



            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInicializadorDB dbInicial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            dbInicial.Inicializador();
            //Proveer el TOKEN en base a las credenciales
            app.UseAuthentication();

            //Definir permisos
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {


                //Al cargarse la app automaticamete dirige al endpoint indicado aquí
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");//Patron de conexión al enpoint inicial
                              //Área          Controlador       Método         Parametros
                endpoints.MapRazorPages();
            });
        }
    }
}
