using Blog.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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


            //Aqui se esta haciedo un cast de tipo IIdentity a ClaimsIdentity se puede castear debido a que implementa de esa interfaz
          
                                                
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;


            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_contenedorTrabajo.Usuario.GetAll(u=>u.Id!= usuarioActual.Value));
        }



        
        public IActionResult Bloquear(string id)
        {
            if(id == null)
            {

                return NotFound();

            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));



        }



        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {

                return NotFound();

            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));



        }








    }
}
