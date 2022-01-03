using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Models
{
   public class Categoria
    {

        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "Es necesario el Nombre")]
        [Display(Name ="Nombre Categoría")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Orden de visualización")]
        public int Orden { get; set; }




    }
}
