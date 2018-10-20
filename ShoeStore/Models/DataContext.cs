namespace ShoeStore.Models
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

        public DbSet<State> States { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        public DbSet<Colony> Colonies { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<User> Users { get; set; }

    }
}