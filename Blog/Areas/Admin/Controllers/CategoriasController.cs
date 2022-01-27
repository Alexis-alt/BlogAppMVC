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

            if (ModelState.IsValid)
            {

                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();


                return RedirectToAction(nameof(Index));

            }


            return View(categoria);

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();

            categoria = _contenedorTrabajo.Categoria.Get(id);

            if(categoria == null)
            {
                return NotFound();


            }

            return View(categoria);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {

            if (ModelState.IsValid)
            {

                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();


                return RedirectToAction(nameof(Index));

            }


            return View(categoria);

        }







        #region Llamadas por AJAX a la API

        [HttpGet]
        public IActionResult GetAll()
        {
            //Convertimos a JSON un objeto anonimo
            return Json(new
            {

            data= _contenedorTrabajo.Categoria.GetAll()

            });

        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if(objFromDb == null)
            {

                return Json(new {
                success = false,
                message = "Error al borrar la categoría"
                
                });
            }


            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();

            return Json(new
            {
                success = true, 
                message = "La categoría fue borrada con exito"

            });



        }





        #endregion








    }
}
