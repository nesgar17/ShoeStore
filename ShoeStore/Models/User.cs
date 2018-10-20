

namespace ShoeStore.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public int IdState { get; set; }

        public int IdMunicipality { get; set; }

        public int IdColony { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public virtual State State { get; set; }

        public virtual Municipality Municipality { get; set; }

        public virtual Colony Colony { get; set; }

      


    }
}
