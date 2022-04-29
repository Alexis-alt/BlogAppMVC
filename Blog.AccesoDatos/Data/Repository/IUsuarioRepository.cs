using Blog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{                                       

                                        //Indicando esto, establecemos que todas las clases hijas o clases de entidades heredaran de la clase padre Repository
                                        //Indicamos el modelo al cual se apegaran los métodos CRUD implementados por repository

    public interface IUsuarioRepository:IRepository<ApplicationUser>
    {

        void BloquearUsuario(string IdUsuario);

        void DesbloquearUsuario(string IdUsuario);


    }
}
