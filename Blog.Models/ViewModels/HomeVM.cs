using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models.ViewModels
{
    public class HomeVM
    {
        //In this Model we specify the necessary models to the view Home due to it requires more than one model because shows both Sliders and Articulos

        public IEnumerable<Slider> Sliders { get; set; }

        public IEnumerable<Articulo> Articulos { get; set; }


    }
}
