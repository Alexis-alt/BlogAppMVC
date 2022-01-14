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

    //Carga la vista con el DataTable
        public IActionResult Index()
        {
            
            return View();


        }



        //Carga la vista para crear un Nuevo Articulo 
        //Completa el dropDown con las categorias y sus items
        [HttpGet]

        public  IActionResult Create()
        {

            ArticuloVM artivm = new ArticuloVM()
            {
                
                Articulo = new Models.Articulo(),
                
                //Se llena con un SelectListItem el cual contiene Nombres y Id de las categorias
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias(),


            };


            //Se puede mandar data a la vista mediante ViweBag o por parametros 

            return View(artivm);
        }


        

        #region Llamadas por AJAX a la API


        //Este método se encarga de proveer los datos al DataTable
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
