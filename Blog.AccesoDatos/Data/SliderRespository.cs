using Blog.AccesoDatos.Data.Repository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data
{
   public class SliderRespository:Repository<Slider>, ISliderRepository
    {


        private readonly ApplicationDbContext _db;


        public SliderRespository(ApplicationDbContext db) : base(db)
        {

            _db = db;

        }





        public void Update(Slider slider)
        {

            var sliderActualizar = _db.Slider.Find(slider.Id);

            sliderActualizar.Nombre = slider.Nombre;
            sliderActualizar.Estado = slider.Estado;
            sliderActualizar.UrlImagen = slider.UrlImagen;

            _db.SaveChanges();
           
        }
         


    }
}
