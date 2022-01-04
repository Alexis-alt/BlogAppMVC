using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin
{
    //Hay que especificar el area a la cual pertenece, para que a la hora de llamarlo no exista ningun conflicto con la ruta
   //Las peticiones de info se estarán trabajando con AJAX desde JS
   //En la vista
    [Area("Admin")]

    public class CategoriasController : Controller
    {

        //Hay que hacer la inyección de dependencias para que se detecte la Interfaz aquí inyectada
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {

            _contenedorTrabajo = contenedorTrabajo;

        }


        public IActionResult Index()
        {
            return View();
     
        }


        //Solicitamos la vista donde se muestra el forma para crear un nuevo elemento
        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {

            //Si modelo obtenido desde el form cumple con todos los requisitos establecidos en los DataAnnotaciones
            if (ModelState.IsValid)
            {
                //Isertción del registro
                _contenedorTrabajo.Categoria.Add(categoria);


                //Se guardan cambios en BD
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));

            }


            return View(categoria);

        }



        #region Llamadas CRUD


        public IActionResult GetAll()
        {
            //Convertimos a JSON un objeto anonimo


            var values = Json(new
            {

                data = _contenedorTrabajo.Categoria.GetAll()

            });


            return values;
        }




        #endregion








    }
}
