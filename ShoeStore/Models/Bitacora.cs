namespace ShoeStore.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Bitacora
    {
        
        [Key]
        public int BitacoraId { get; set; }

        public string Accion { get; set; }

        public string Table { get; set; }

        public string User { get; set; }

        public DateTime DateofInsert { get; set; }

      
    }
}