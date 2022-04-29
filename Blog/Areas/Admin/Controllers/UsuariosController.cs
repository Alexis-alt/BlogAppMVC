using Blog.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UsuariosController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {

            _contenedorTrabajo = contenedorTrabajo;

        }



        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;

            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_contenedorTrabajo.Usuario.GetAll(u=>u.Id!= usuarioActual.Value));
        }
















    }
}
