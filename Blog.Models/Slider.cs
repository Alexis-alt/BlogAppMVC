using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesario el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Es necesario establecer un estado")]
        public bool Estado { get; set; }

      
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen{ get; set; }
    }
}
