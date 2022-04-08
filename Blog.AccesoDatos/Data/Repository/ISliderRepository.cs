using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface ISliderRepository: IRepository<Slider>
    {

        public void Update(Slider slider);


    }
}
