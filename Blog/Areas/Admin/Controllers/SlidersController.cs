using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        private readonly IWebHostEnvironment _hostEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostEnvironment)
        {

            _contenedorTrabajo = contenedorTrabajo;

            _hostEnvironment = hostEnvironment;


        }

        



        public IActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {

                //This property help us to mapping the route to the  file container in the server
                string rutaPrincipal = _hostEnvironment.WebRootPath;

                //We reference files uploaded in the form 
                var files = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {

                    string fileName = Guid.NewGuid().ToString();

                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");

                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(subidas, fileName + extension), FileMode.Create))
                    {


                        files[0].CopyTo(fileStream);



                    }

                    slider.UrlImagen = @"imagenes\sliders\" + fileName + extension;

                    _contenedorTrabajo.Slider.Add(slider);

                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));


                }





            }




            return View(slider);


        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slider slider = new Slider();

            slider = _contenedorTrabajo.Slider.Get(id);

            return View(slider);

        }

        [HttpPost]

        public IActionResult Edit(Slider slider)
        {

            var sliderEditar = _contenedorTrabajo.Slider.Get(slider.Id);

            if (ModelState.IsValid)
            {
                //Podemos obtenerla o establecerla
                string rutaPrincipal = _hostEnvironment.WebRootPath;

                //Referencia los archivos que se cargan en el form
                //Al parecer es un array que contiene todos los archivos cargados
                var archivos = HttpContext.Request.Form.Files;


                if(archivos.Count > 0)
                {

                    string nombreArchivo = Guid.NewGuid().ToString();

                    //Indicamos el repositorio donde se almacenarán los archivos o en este caso las imagenes
                    //Concatenamos o añadimos la rutaPrincipal es decir la ruta del servidor
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");

                    //Extraemos el nombre del archivo cargado y su extensión
                    //Accedemos al array que almacena los archivos cargados en el form, en este caso a la primera pocisión debido a que solo cargamos un archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);


                    var rutaImagen = Path.Combine(rutaPrincipal, sliderEditar.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {

                        System.IO.File.Delete(rutaImagen);

                    }

                    using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {


                        archivos[0].CopyTo(fileStream);



                    }


                    slider.UrlImagen = @"imagenes\sliders\" + nombreArchivo + extension;

                    _contenedorTrabajo.Slider.Update(slider);

                    _contenedorTrabajo.Save();


                    return RedirectToAction(nameof(Index));


                }
                else
                {



                    slider.UrlImagen = sliderEditar.UrlImagen;
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));



                }




            }



            return View();

        }




        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var sliderEliminar = _contenedorTrabajo.Slider.Get(id);

            var rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;

            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderEliminar.UrlImagen.TrimStart('\\'));


            if (System.IO.File.Exists(rutaImagen))
            {

                System.IO.File.Delete(rutaImagen);

            }

            if (sliderEliminar == null)
            {


                return Json(new { success = false, message = "Something was Wrong :(" });
            }


            _contenedorTrabajo.Slider.Remove(sliderEliminar);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borro correctamente" });










        }






        #region Querys API

        [HttpGet] 


        public IActionResult GetAll()
        {
            var response = Json(new
            {

                //Accede primero a la unidad contenedor de trabajo donse se encuentran las propiedades que referencian al rspositorio de cada model
                data = _contenedorTrabajo.Slider.GetAll()

            });

            return response;


        }






        #endregion



    }
}
