using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoeStore.Models
{
    public class Zapateria
    {

        [Key]
        public int ZapateriaId { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre de la Zapateria")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Semblanza")]
        [DataType(DataType.MultilineText)]
        public string Descritcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Fundación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        public int IdState { get; set; }

        public int IdMunicipality { get; set; }

        public int IdColony { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

       // [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }


        [Display(Name = "Dirección")]
        public string FullAddress { get { return string.Format("{0} {1} {2} {3}", State.Description, Municipality.Description, Colony.Description, Address); } }


        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        public int OwnerId { get; set; }


        public virtual State State { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual Colony Colony { get; set; }

        public virtual Owner Owner { get; set; }



    }
}