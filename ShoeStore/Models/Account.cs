namespace ShoeStore.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        public string Benefit { get; set; }

        public int DaysDuration { get; set; }

        public int OwnerId { get; set; }

        public virtual Owner Owner { get; set; }


    }
}