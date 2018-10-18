
namespace ShoeStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Municipality
    {

        [Key]
        public int IdMunicipality { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        [StringLength(60, ErrorMessage = "El campo {0} debe tener minimo {1} y maximo {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Description { get; set; }

        public int IdState { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Colony> Colonies { get; set; }

        //public virtual ICollection<Administrador> administradores { get; set; }

        //public virtual ICollection<Zapateria> Zapaterias { get; set; }

    }
}