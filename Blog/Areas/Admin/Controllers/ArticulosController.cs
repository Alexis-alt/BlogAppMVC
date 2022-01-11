using Blog.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{


    [Area("Admin")]

    public class ArticulosController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo)
        {

            _contenedorTrabajo = contenedorTrabajo;


        }


        public IActionResult Index()
        {
            return View();
        }




        

        #region Llamadas por AJAX a la API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Convertimos a JSON un objeto anonimo
            return Json(new
            {

            data= _contenedorTrabajo.Articulo.GetAll(includeProperties:"Categoria")

            });

        }







        #endregion






    }
}
