using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {


        //Propiedades que referencian a cada una de las Entidades que conforman
        ICategoriaRepository Categoria { get; }

        IArticuloRepository Articulo { get; }

        ISliderRepository Slider { get; }

        void Save();


    }
}
