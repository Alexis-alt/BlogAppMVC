using Blog.AccesoDatos.Data.Repository;
using Blog.Models.ViewModels;
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
    //Se usa ArticuloVM como Modelo debido a que se adapto para contener un modelo de Tipo Articulo y una Lista de Categorias adecuadas para el DropDowm

    [Authorize]
    [Area("Admin")]

    public class ArticulosController : Controller
    {
        //Solo implementa métodos y propiedades que contiene la Interfaz IContenedorTrabajo

        private readonly IContenedorTrabajo _contenedorTrabajo;

        private readonly IWebHostEnvironment _hostEnvironment;

       
        //Constructor
        //Los  parametros que se reciben en este constructor, en realidad son inyecciones de las cuales se hace uso en los métodos del controller
       
        public ArticulosController(IContenedorTrabajo contenedorTrabajo,IWebHostEnvironment hostEnvironment)
        {

            _contenedorTrabajo = contenedorTrabajo;

            _hostEnvironment = hostEnvironment;

            
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

        [HttpPost]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                //Property que Mapea la ruta del servidor donde se encuentra el repositorio contenedor de archivos (wwwroot)
                //Podemos obtenerla o establecerla
                string rutaPrincipal = _hostEnvironment.WebRootPath;

                //Referencia los archivos que se cargan en el form
                //Al parecer es un array que contiene todos los archivos cargados
                var archivos = HttpContext.Request.Form.Files;

                /*Al introducir un nuevo articulo, este no recibe valor en su ID hasta que llega a la base de datos
                 * donde ya se especificó que el ID es auto incrementable (Si hicimos code first se agrega esa caracteristica de manera automatica, si hicimopps databasefirst entonces hapy que agregarlo manualmente en las proiedades del ID desde su creación)
                 * En este caso solo estamos reuniendo la data  para generar un registro en BD 
                */
                if (artiVM.Articulo.IdArticulo == 0)
                {
                    //Asignamos nombre al del archivo un Guid que nos da como resultado una cadena unica
                    string nombreArchivo = Guid.NewGuid().ToString();

                    //Indicamos el repositorio donde se almacenarán los archivos o en este caso las imagenes
                    //Concatenamos o añadimos la rutaPrincipal es decir la ruta del servidor
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");

                    //Extraemos el nombre del archivo cargado y su extensión
                    //Accedemos al array que almacena los archivos cargados en el form, en este caso a la primera pocisión debido a que solo cargamos un archivo
                    var extension = Path.GetExtension(archivos[0].FileName);


                    //Creamos un contexto para crear nuestro archivo 
                    //Instanciamos un objeto de tipo FileStream el cual recibe 4 parametros en el Constructor, 2 obligatorios que son los que enviamos
                   
                                                           //Ruta del archivo -String                   //Que se va a hacer -Enum
                    using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo+extension),FileMode.Create))
                    {


                        archivos[0].CopyTo(fileStream);


                        
                    }

                    //Añadimos los valores de los atributos restantes del modelo
                    artiVM.Articulo.UrlImagen = @"imagenes\articulos\" + nombreArchivo + extension;

                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();


                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }




            }



            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();


            return View(artiVM);

        }
        

        [HttpGet]

       public IActionResult Edit(int? id)
        {

            ArticuloVM articuloVM = new ArticuloVM();

            articuloVM.Articulo = new Models.Articulo();
            articuloVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

           if(id != null)
            {

                articuloVM.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());

            }



            return View(articuloVM);


        }
     

        [HttpPost]

        public IActionResult Edit(ArticuloVM artiVM)
        {

            var articuloEditar = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.IdArticulo);



            if (ModelState.IsValid)
            {
                //Property que Mapea la ruta del servidor donde se encuentra el repositorio contenedor de archivos (wwwroot)
                //Podemos obtenerla o establecerla
                string rutaPrincipal = _hostEnvironment.WebRootPath;

                //Referencia los archivos que se cargan en el form
                //Al parecer es un array que contiene todos los archivos cargados
                var archivos = HttpContext.Request.Form.Files;


                if (archivos.Count() > 0)
                {
                    //Asignamos nombre al del archivo un Guid que nos da como resultado una cadena unica
                    string nombreArchivo = Guid.NewGuid().ToString();

                    //Indicamos el repositorio donde se almacenarán los archivos o en este caso las imagenes
                    //Concatenamos o añadimos la rutaPrincipal es decir la ruta del servidor
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");

                    //Extraemos el nombre del archivo cargado y su extensión
                    //Accedemos al array que almacena los archivos cargados en el form, en este caso a la primera pocisión debido a que solo cargamos un archivo
                    var extension = Path.GetExtension(archivos[0].FileName);

                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);


                    var rutaImagen = Path.Combine(rutaPrincipal, articuloEditar.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {

                        System.IO.File.Delete(rutaImagen);

                    }


                    //Creamos un contexto para crear nuestro archivo 
                    //Instanciamos un objeto de tipo FileStream el cual recibe 4 parametros en el Constructor, 2 obligatorios que son los que enviamos

                    //Ruta del archivo -String                   //Que se va a hacer -Enum
                    using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {


                        archivos[0].CopyTo(fileStream);



                    }

                    //Añadimos los valores de los atributos restantes del modelo
                    artiVM.Articulo.UrlImagen = @"imagenes\articulos\" + nombreArchivo + extension;

                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();


                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    //Cuando la imagen no se tiene que editar y se tiene que conservar la misma de la BD

                    artiVM.Articulo.UrlImagen = articuloEditar.UrlImagen;


                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));

                }





            }



            return View();



        }


        [HttpDelete]

        public IActionResult Delete(int id)
        {

            var articuloEliminar = _contenedorTrabajo.Articulo.Get(id);

            var rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal,articuloEliminar.UrlImagen.TrimStart('\\'));


            if (System.IO.File.Exists(rutaImagen))
            {

                System.IO.File.Delete(rutaImagen);

            }

            if (articuloEliminar == null)
            {


                return Json(new {success=false, message = "Something was Wrong :(" });
            }


            _contenedorTrabajo.Articulo.Remove(articuloEliminar);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borro correctamente" });



        }



        #region Llamadas por AJAX a la API


        //Este método se encarga de proveer los datos al DataTable
        [HttpGet]  
        public IActionResult GetAll()
        {
            //Convertimos a JSON un objeto anonimo
            var response = Json(new
            {

                //Accede primero a la unidad contenedor de trabajo donse se encuentran las propiedades que referencian al rspositorio de cada model
            data= _contenedorTrabajo.Articulo.GetAll(includeProperties:"Categoria")

            });

            return response;

        }



        #endregion






    }
}
