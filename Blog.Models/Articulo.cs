using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Models
{
   public class Articulo
    {
        [Key]
        public int IdAriculo { get; set; }

        [Required(ErrorMessage ="El nombre es necesario")]
        [Display(Name ="Nombre del articulo")]
        public string Nombre { get; set; }



        [Required(ErrorMessage = "La descripción es necesaria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }


        [Display(Name = "Fecha de creación")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }


        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }


    }
}
