using Blog.AccesoDatos.Data.Repository;
using Blog.Models.ViewModels;
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


        [HttpGet]

        public  IActionResult Create()
        {

            ArticuloVM artivm = new ArticuloVM()
            {

                Articulo = new Models.Articulo(),

                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),


            };

            return View(artivm);
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
