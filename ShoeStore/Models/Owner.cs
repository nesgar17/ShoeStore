namespace ShoeStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class Owner
    {

        [Key]
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre(s)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

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

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        public virtual State State { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual Colony Colony { get; set; }


    }
}