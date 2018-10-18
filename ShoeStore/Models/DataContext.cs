﻿namespace ShoeStore.Models
{

    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {

        public DataContext() : base ("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<ShoeStore.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<ShoeStore.Models.Municipality> Municipalities { get; set; }

        public System.Data.Entity.DbSet<ShoeStore.Models.Colony> Colonies { get; set; }
    }
}