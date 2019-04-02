namespace DAL.EF
{
    using DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class CatalogContext : DbContext
    {
        // Your context has been configured to use a 'CatalogContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DAL.EF.CatalogContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CatalogContext' 
        // connection string in the application configuration file.
        public CatalogContext()
            : base("name=CatalogContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Brand>()
                .HasMany<Model>(c => c.Models)
                .WithRequired(x => x.Brand)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Model>()
                .HasMany<Car>(c => c.Cars)
                .WithRequired(x => x.Model)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Car>()
                .HasMany<Cost>(c => c.Prices)
                .WithRequired(x => x.Car)
                .WillCascadeOnDelete(true);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Cost> Prices { get; set; }
    }
}